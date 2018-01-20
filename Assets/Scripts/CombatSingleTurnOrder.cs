using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatSingleTurnOrder : MonoBehaviour {
    public CombatTurnPortrait turnPortraitPrefab;
    public Transform portraitParent;
    public Text text;
    List<CombatTurnPortrait> portraits = new List<CombatTurnPortrait>();

    public void Setup(List<CombatController> characters, int turnStackIndex)
    {
        Clear();
        characters.ForEach(c => CreatePortrait(c, turnStackIndex));
    }

    public void SetHeader(string header)
    {
        text.text = header;
    }

    public void Clear()
    {
        portraits.Clear();
        foreach (Transform t in portraitParent.transform)
            GameObject.Destroy(t.gameObject);
    }

    GameObject CreatePortrait(CombatController c, int turnStackIndex)
    {
        var go = GameObject.Instantiate(turnPortraitPrefab.gameObject);
        go.transform.SetParent(portraitParent);
        var portrait = go.GetComponent<CombatTurnPortrait>();
        portrait.Setup(c, c.GetInitiative());
        portraits.Add(portrait);
        return go;
    }

    public void SetCombatControllerActive(CombatController c)
    {
        portraits.ForEach(p => p.SetActiveVisuals(p.character == c));
    }
}
