using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

#warning "this class use to be part of turn manager, which should only be used in combat. It needs to know when to run on its own.
public class MapPlayerView : DesertView {
	public GridHighlighter gridHighlighter;
	public GameObject characterPrefab;
	public float travelTime = 0.25f;

	GameObject characterGO;
	public GameObject CharacterGO { get { return characterGO; }}

	public void Setup() {
		characterGO = GameObject.Instantiate(characterPrefab) as GameObject;
		characterGO.transform.SetParent(transform);
	}

	public void DrawPath(List<Vector2> path) {
		gridHighlighter.DrawPath(path);
	}

	//TODO: I removed the speed mod. I should readd it somewhere else.
	public void Move(Vector2 position, System.Action finishedMove) {
		AnimationController.Move(characterGO, position, () => FinishedMove(finishedMove), 1.0f/*speed mod goes here*/);
	}

	void FinishedMove(System.Action finishedMove) {
		finishedMove();
	}

	public void StopMovement(Vector2 destination) {
		LeanTween.cancel(characterGO);
		StopAllCoroutines();
		Teleport (destination);
	}

	public void Teleport(Vector2 destination) {
		characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)destination.x, 
		                                                                               (int)destination.y);
	}
}

public class MapPlayerViewMediator : Mediator {
	[Inject] public MapPlayerView view { private get; set; }
	[Inject] public MapPlayerController controller {private get; set; }
	[Inject] public GridInputCollector gridInputCollector {private get; set; }

	public override void OnRegister ()
	{
		view.Setup();
		view.Teleport(controller.position);

		gridInputCollector.mouserOverPositionEvent += MouseOver;

		controller.movementStopped.AddListener(StopMovementOnView);
		controller.animateMovement.AddListener(view.Move);
		controller.teleportEvent += view.Teleport;
	}

	public override void OnRemove ()
	{
		gridInputCollector.mouserOverPositionEvent -= MouseOver;

		controller.movementStopped.RemoveListener(StopMovementOnView);
		controller.animateMovement.RemoveListener(view.Move);
		controller.teleportEvent -= view.Teleport;
	}
	
	void MouseOver(Vector2 destination) {
		if(!controller.isPathing)
			view.DrawPath(controller.GetPathToPosition(destination));
	}

	void StopMovementOnView() {
		view.StopMovement(controller.position);
	}
}
