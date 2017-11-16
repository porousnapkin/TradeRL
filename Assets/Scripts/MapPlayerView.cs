using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class MapPlayerView : DesertView {
	public GridHighlighter gridHighlighter;
	public GameObject characterPrefab;
	public float slomoMod = 0.5f;

    bool moving = false;
    System.Action moveCallback = null;
    Vector2 moveToPos = Vector2.zero;
	GameObject characterGO;
	public GameObject CharacterGO { get { return characterGO; }}

	public void Setup() {
		characterGO = GameObject.Instantiate(characterPrefab) as GameObject;
		characterGO.transform.SetParent(transform);
	}

	public void DrawPath(List<Vector2> path) {
		gridHighlighter.DrawPath(path);
	}

	public void Move(Vector2 position, System.Action finishedMove) {
        moving = true;
        moveToPos = position;
        moveCallback = finishedMove;
        AnimationController.Move(characterGO, position, () => FinishedMove(moveCallback));
	}

    public void SlomoCurrentMovement()
    {
        if (!moving)
            return;

		LeanTween.cancel(characterGO);
        AnimationController.Move(characterGO, moveToPos, () => FinishedMove(moveCallback), slomoMod);
    }

	void FinishedMove(System.Action finishedMove) {
        moving = false;
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

	public void Die() {
		gameObject.SetActive(false);
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
        controller.slomoMovement.AddListener(view.SlomoCurrentMovement);
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
