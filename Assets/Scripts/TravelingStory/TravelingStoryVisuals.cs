using UnityEngine;
using strange.extensions.mediation.impl;

public class TravelingStoryVisuals : DesertView {
	public SpriteRenderer sprite;

	public void Setup(Sprite art) {
		sprite.sprite = art;
	}

	public void MoveToNewPosition (Vector2 position) {
		LeanTween.cancel(gameObject);
		AnimationController.Move(gameObject, position);
	}

	public void Removed() {
		GameObject.Destroy(gameObject);
	}

	public void TeleportToWorldPosition(Vector2 position) {
		transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
	}

	public void SetVisible(bool visible) {
		gameObject.SetActive(visible);
	}
}

public class TravelingStoryVisualsMediator : Mediator {
	[Inject] public TravelingStoryVisuals view { private get; set; }
	[Inject] public TravelingStory model { private get; set; }

	public override void OnRegister ()
	{
		model.movingToNewPositionSignal.AddListener(view.MoveToNewPosition);
		model.removeSignal.AddListener(view.Removed);
		model.teleportSignal.AddListener(view.TeleportToWorldPosition);
		model.isVisibleSignal.AddListener(view.SetVisible);
	}

	public override void OnRemove() 
	{
		model.movingToNewPositionSignal.RemoveListener(view.MoveToNewPosition);
		model.removeSignal.RemoveListener(view.Removed);
		model.teleportSignal.RemoveListener(view.TeleportToWorldPosition);
		model.isVisibleSignal.AddListener(view.SetVisible);
	}
}


