using UnityEngine;

public class ShieldGeneratorEffect
{
    public int shieldGeneratedPerTurn = 4;
    public int maxShieldGeneratable = 4;
    Character character;

    public void Apply(Character character)
    {
        this.character = character;

        GlobalEvents.CombatantTurnStart += CheckForTurnBegan;
        GlobalEvents.CombatEnded += FlushShield;
    }

    public void Remove()
    {
        GlobalEvents.CombatantTurnStart -= CheckForTurnBegan;
        GlobalEvents.CombatEnded -= FlushShield;
    }

    void CheckForTurnBegan(Character c)
    {
        if (character != c)
            return;
        var shieldMod = ShieldDefenseMod.GetFrom(character);
        var amountToAdd = Mathf.Min(maxShieldGeneratable - shieldMod.Value, shieldGeneratedPerTurn);
        if (amountToAdd > 0)
            shieldMod.Value += amountToAdd;
    }

    private void FlushShield()
    {
        var shieldMod = ShieldDefenseMod.GetFrom(character);
        shieldMod.Value = 0;
    }
}
