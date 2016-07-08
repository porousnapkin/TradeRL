using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatTurnPortrait : MonoBehaviour {
    public CombatController character;
    public Image border;
    public Image characterArt;
    public Color playerColor;
    public Color enemyColor;
    public GameObject activeVisuals;
    public Text initiativeDisplay;

    public void Setup(CombatController character, int initiative) {
        this.character = character;
        characterArt.sprite = character.character.ownerGO.GetComponent<SpriteRenderer>().sprite;
        if (character.character.myFaction == Faction.Player)
            border.color = playerColor;
        else
            border.color = enemyColor;

        initiativeDisplay.text = initiative.ToString();
        activeVisuals.SetActive(false);
    }

    public void SetActiveVisuals(bool on)
    {
        activeVisuals.SetActive(on);
    }
}
