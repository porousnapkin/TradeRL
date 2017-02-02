using System;
using UnityEngine;

public class CanRestRestriction : Restriction
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public bool CanUse()
    {
        return playerCharacter.CanRest();
    }
}