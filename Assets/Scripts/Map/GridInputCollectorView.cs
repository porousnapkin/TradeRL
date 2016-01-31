using UnityEngine;
using System;
using strange.extensions.mediation.impl;

public class GridInputCollectorView : DesertView {
	public event Action<Vector2> mouseOver = delegate{};
	public event Action<Vector2> mouseClicked = delegate{};

	public LayerMask layerMask;
	GridInputPosition activePoint;
	System.Action<Vector2> overrideMouseHitCallback = delegate{};
	bool inputOverriden = false;

	public void SetActivePoint(GridInputPosition position) {
		activePoint = position;
		GridHighlighter.Instance.MoveMouseOverImage(activePoint.position);
		if(!inputOverriden)
			mouseOver(activePoint.position);
	}

	public void PointClicked(GridInputPosition position) {
		activePoint = position;
		 if(inputOverriden)
			overrideMouseHitCallback(activePoint.position);
		else
			mouseClicked(activePoint.position);
	}

	public void OverrideInput(System.Action<Vector2> mouseHitCallback) {
		overrideMouseHitCallback = mouseHitCallback;
		inputOverriden = true;
	}

	public void FinishOverridingInput() {
		inputOverriden = false;
	}
}

public class GridInputCollectorMediator : Mediator {
	[Inject] public GridInputCollectorView view {private get; set; }
	[Inject] public GridInputCollector gridInputCollector {private get; set; }

	public override void OnRegister ()
	{
		view.mouseOver += gridInputCollector.MouseOverPosition;
		view.mouseClicked += gridInputCollector.MouseClickedPosition;

		gridInputCollector.overrideInputEvent += view.OverrideInput;
		gridInputCollector.finishOverridingInputEvent += view.FinishOverridingInput;
	}

	public override void OnRemove ()
	{
		view.mouseOver -= gridInputCollector.MouseOverPosition;
		view.mouseClicked -= gridInputCollector.MouseClickedPosition;
		
		gridInputCollector.overrideInputEvent -= view.OverrideInput;
		gridInputCollector.finishOverridingInputEvent -= view.FinishOverridingInput;
	}
}

#warning "I think there's functionality to move around here."
public class GridInputCollector {
	public event Action<Vector2> mouserOverPositionEvent = delegate{};
	public event Action<Vector2> mouseClickedPositionEvent = delegate{};
	
	public event Action< Action<Vector2> > overrideInputEvent = delegate{};
	public event Action finishOverridingInputEvent = delegate{};
	
	public void MouseOverPosition(Vector2 pos) {
		mouserOverPositionEvent(pos);
	}
	
	public void MouseClickedPosition(Vector2 pos) {
		mouseClickedPositionEvent(pos);
	}
	
	public void OverrideInput(Action<Vector2> mouseClickedCallback) {
		overrideInputEvent(mouseClickedCallback);
	}
	
	public void FinishOverridingInput() {
		finishOverridingInputEvent();
	}
}
