using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TownOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public Text text;
	public Image buttonImage;
	[HideInInspector]public Town startTown;
	[HideInInspector]public Town representedTown;

	public event System.Action<TownOption> TownSelectedEvent = delegate{};
	bool selected = false;

	void Start() {
		var daysAway = Mathf.RoundToInt(Vector3.Distance(startTown.worldPosition, representedTown.worldPosition));
		text.text = representedTown.name + " (" + daysAway + " days away, " + representedTown.goodsDemanded + "/" + representedTown.MaxGoodsDemanded + " demand)";
	}

	public void OnPointerEnter(PointerEventData data) {
		if(!selected)
			buttonImage.color = new Color(1, 1, 1, 1);
	}

	public void OnPointerExit(PointerEventData data) {
		if(!selected)
			buttonImage.color = new Color(1, 1, 1, 0);
	}

	public void OnPointerClick(PointerEventData data) {
		Select();	
	}

	public void Select() {
		selected = true;
		buttonImage.color = new Color(1, 0.5f, 0.5f, 1);
		TownSelectedEvent(this);
	}

	public void OnTownOptionSelected(TownOption option) {
		if(option == this)
			return;

		selected = false;
		OnPointerExit(null);
	}
}
