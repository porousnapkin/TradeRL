using System.Collections.Generic;

public class DefenseModule {
    public int damageReduction = 0;

    public int GetDamageReduction()
    {
        return damageReduction;
    }

    public void ModifyIncomingAttack(AttackData data)
    {
        if (damageReduction > 0)
        {
            data.damageModifiers.Add(new DamageModifierData
            {
                damageMod = -damageReduction,
                damageModSource = "damage reduction"
            });
        }
    }
}
