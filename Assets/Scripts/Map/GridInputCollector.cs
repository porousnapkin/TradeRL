using UnityEngine;

public class GridInputCollector : MonoBehaviour {
	public LayerMask layerMask;
	public GridHighlighter gridHighlighter;
	GridInputPosition activePoint;
	System.Action<Vector2> overrideMouseHitCallback = delegate{};
	bool inputOverriden = false;

	public void SetActivePoint(GridInputPosition position) {
		Debug.Log (position.position);
		activePoint = position;
		gridHighlighter.MoveMouseOverImage(activePoint.position);
		if(!inputOverriden)
			PlayerController.Instance.MouseOverPoint(activePoint.position);
	}

	public void PointClicked(GridInputPosition position) {
		activePoint = position;
		 if(inputOverriden)
			overrideMouseHitCallback(activePoint.position);
		else
			PlayerController.Instance.ClickedOnPosition(activePoint.position);
	}

	public void OverrideInput(System.Action<Vector2> mouseHitCallback) {
		overrideMouseHitCallback = mouseHitCallback;
		inputOverriden = true;
	}

	public void FinishOverridingInput() {
		inputOverriden = false;
	}
}
