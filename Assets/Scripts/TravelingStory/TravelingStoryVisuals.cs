using UnityEngine;
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
	[Inject] public TravelingStory model { private get; set; }

	public override void OnRegister ()
	{
		model.movingToNewPositionSignal.AddListener(view.MoveToNewPosition);
		model.activatedSignal.AddListener(view.Activated);
		model.teleportSignal.AddListener(view.TeleportToWorldPosition);
	}

	public override void OnRemove() 
	{
		model.movingToNewPositionSignal.RemoveListener(view.MoveToNewPosition);
		model.activatedSignal.RemoveListener(view.Activated);
		model.teleportSignal.RemoveListener(view.TeleportToWorldPosition);
	}
}


