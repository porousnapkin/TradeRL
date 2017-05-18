using System;
using UnityEngine;

public class HasXInitiativeRestriction : Restriction, Visualizer
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
    public int initiativeRequriement { private get; set; }

    public bool CanUse()
    {
        var controller = playerCharacter.GetCharacter().controller;
        return controller != null && controller.GetInitiative(0) >= initiativeRequriement;
    }

    public void SetupVisualization(GameObject go)
    {
        var drawer = go.AddComponent<GenericRestrictionDrawer>();
        drawer.text = "Need at least " + 8 + " initative to use";
        drawer.restriction = this;
    }
}
