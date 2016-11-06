using System.Collections.Generic;

public interface AttackModifier
{
    void ModifyAttack(AttackData attack);
}

public class AttackModifierSet
{
    Dictionary<string, AttackModifier> attackModifiers = new Dictionary<string, AttackModifier>();

    public AttackModifier GetActiveModifier(string name, System.Func<AttackModifier> constructor)
    {
        AttackModifier outVal;
        bool foundVal = attackModifiers.TryGetValue(name, out outVal);
        if(!foundVal)
        {
            outVal = constructor();
            attackModifiers[name] = outVal;
        }

        return outVal;
    }

    public void ApplyAttackModifier(AttackData attack)
    {
        foreach (var pair in attackModifiers)
            pair.Value.ModifyAttack(attack);
    }
}

