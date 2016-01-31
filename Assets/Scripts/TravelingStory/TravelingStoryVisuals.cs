using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class TravelingStoryVisuals : DesertView {
	public SpriteRenderer sprite;

	public void Setup(Sprite art) {
		sprite.sprite = art;
	}

	public void MoveToNewPosition (Vector2 position) {
		LeanTween.cancel(gameObject);
		AnimationController.Move(gameObject, position);
	}

	public void Activated() {
		GameObject.Destroy(gameObject);
	}

	public void TeleportToWorldPosition(Vector2 position) {
		transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
	}
}

public class TravelingStoryVisualsMediator : Mediator {
	[Inject] public TravelingStoryVisuals view { private get; set; }
	[Inject] public TravelingStory story { private get; set; }

	public override void OnRegister ()
	{
		story.movingToNewPositionSignal.AddListener(view.MoveToNewPosition);
		story.activatedSignal.AddListener(view.Activated);
		story.teleportSignal.AddListener(view.TeleportToWorldPosition);

		view.Setup(story.data.art);
		view.TeleportToWorldPosition(story.WorldPosition);
	}

	public override void OnRemove() 
	{
		story.movingToNewPositionSignal.RemoveListener(view.MoveToNewPosition);
		story.activatedSignal.RemoveListener(view.Activated);
		story.teleportSignal.RemoveListener(view.TeleportToWorldPosition);
	}
}

public class TravelingStory {
	[Inject] public StoryFactory storyFactory { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public EncounterFactory encounterFactory { private get; set; }

	public TravelingStoryData data { get; set; }
	public Vector2 WorldPosition { 
		get { return position; }
		set {
			mapGraph.TravelingStoryVacatesPosition(position);
			mapGraph.RemoveEventAtLocation((int)position.x, (int)position.y);
			position = value;

			if(mapGraph.playerPosition == position) {
				Activate(() => {});
			}
			else {
				mapGraph.SetEventForLocation((int)position.x, (int)position.y, (f) => Activate(f));
				mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
			}
		}
	}

	Vector2 position;
	public Signal<Vector2> movingToNewPositionSignal = new Signal<Vector2>();
	public Signal activatedSignal = new Signal();
	public Signal<Vector2> teleportSignal = new Signal<Vector2>();

	[PostConstruct]
	public void Setup () {
		gameDate.DaysPassedEvent += HandleDaysPassed;
	}
	
	~TravelingStory() {
		gameDate.DaysPassedEvent -= HandleDaysPassed;
	}
	
	void HandleDaysPassed (int days) {
		WorldPosition = GetMoveToPosition();
		movingToNewPositionSignal.Dispatch(WorldPosition);
	}
	
	Vector2 GetMoveToPosition() {
		var offset = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		var newPosition = position + offset;
		
		if(!Grid.IsValidPosition((int)newPosition.x, (int)newPosition.y) ||
		   mapGraph.GetTravelingStoryAtLocation(newPosition) != null)
			return GetMoveToPosition();
		return newPosition;
	}
	
	void Activate(System.Action finishedDelegate) {
		switch(data.stepInAction) {
		case TravelingStoryData.StepInAction.BeginStory:
			storyFactory.CreateStory(data.story, finishedDelegate); break;
		case TravelingStoryData.StepInAction.Combat:
			encounterFactory.CreateEncounter(data.combatData); break;
		}

		activatedSignal.Dispatch();
	}
	
	public void TeleportToWorldPosition() {
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		teleportSignal.Dispatch(WorldPosition);
	}
}


