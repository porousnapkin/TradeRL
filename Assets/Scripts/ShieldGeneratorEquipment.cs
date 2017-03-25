using UnityEngine;

public class ShieldGeneratorEquipment : ItemEffect
{
    public int shieldGeneratedPerTurn = 4;
    public int maxShieldGeneratable = 4;
    private Character character;

    public void Equip(Character character)
    {
        this.character = character;

        GlobalEvents.CombatantTurnStart += CheckForTurnBegan;
    }

    public void UnEquip(Character character)
    {
        GlobalEvents.CombatantTurnStart -= CheckForTurnBegan;
    }

    public void CheckForTurnBegan(Character c)
    {
        if (character == c)
            return;
        var shieldMod = ShieldDefenseMod.GetFrom(character);
        var amountToAdd = Mathf.Min(maxShieldGeneratable - shieldMod.Value, shieldGeneratedPerTurn);
        if (amountToAdd > 0)
            shieldMod.Value += amountToAdd;
    }

    public void NumItemsChanged(int numWas, int newNum) {}
    public bool CanActivate() { return false; }
    public void Activate() {}
    public bool CanEquip() { return true;}
}

