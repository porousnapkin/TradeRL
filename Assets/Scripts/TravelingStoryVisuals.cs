using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class TravelingStoryVisuals : DesertView {
	public SpriteRenderer sprite;
	public Text aiText;

	public void Setup(Sprite art) {
		sprite.sprite = art;
	}

	public void MoveToNewPosition (Vector2 position) {
		LeanTween.cancel(gameObject);
		AnimationController.Move(gameObject, position);
	}

	public void Removed() {
		if(this != null)
			GameObject.Destroy(gameObject);
	}

	public void TeleportToWorldPosition(Vector2 position) {
		transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
	}

	public void SetVisible(bool visible) {
		gameObject.SetActive(visible);
	}

	public void SetStateToUnknowing() {
		aiText.text = "?";
		aiText.color = new Color(0, 100, 0);
	}

	public void SetStateToKnown() {
		aiText.text = "!";
		aiText.color = Color.red;
	}
}

public class TravelingStoryVisualsMediator : Mediator {
	[Inject] public TravelingStoryVisuals view { private get; set; }
	[Inject] public TravelingStoryMediated model { private get; set; }

	public override void OnRegister ()
	{
		model.movingToNewPositionSignal += view.MoveToNewPosition;
		model.removeSignal += view.Removed;
		model.teleportSignal += view.TeleportToWorldPosition;
		model.isVisibleSignal += view.SetVisible;
		model.runningCloseAI += view.SetStateToKnown;
		model.runningFarAI += view.SetStateToUnknowing;
	}

	public override void OnRemove() 
	{
		model.movingToNewPositionSignal -= view.MoveToNewPosition;
		model.removeSignal -= view.Removed;
		model.teleportSignal -= view.TeleportToWorldPosition;
		model.isVisibleSignal -= view.SetVisible;
		model.runningCloseAI -= view.SetStateToKnown;
		model.runningFarAI -= view.SetStateToUnknowing;
	}
}


