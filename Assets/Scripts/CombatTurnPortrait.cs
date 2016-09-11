using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CombatTurnPortrait : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public CombatController character;
    public Image border;
    public Image characterArt;
    public Color playerColor;
    public Color enemyColor;
    public GameObject activeVisuals;
    public Text initiativeDisplay;
    TargetHighlighter highlighter;

    public void Setup(CombatController character, int initiative) {
        this.character = character;
        characterArt.sprite = character.character.ownerGO.GetComponent<SpriteRenderer>().sprite;
        if (character.character.myFaction == Faction.Player)
            border.color = playerColor;
        else
            border.color = enemyColor;

        initiativeDisplay.text = initiative.ToString();
        activeVisuals.SetActive(false);

        highlighter = DesertContext.StrangeNew<TargetHighlighter>();
    }

    public void SetActiveVisuals(bool on)
    {
        activeVisuals.SetActive(on);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        highlighter.HighlightTargets(new List<Character> ( new Character[1] { character.character} ));
	}

    public void OnPointerExit(PointerEventData eventData) {
        highlighter.RemoveAllHighlights();
	}
}
