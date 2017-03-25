public class DefenseModule {
    public int damageReduction = 0;
	public event System.Action<AttackData> modifyIncomingAttack = delegate{};
    public AttackModifierSet attackModifierSet = new AttackModifierSet();

    public int GetDamageReduction()
    {
        return damageReduction;
    }

    public void ModifyIncomingAttack(AttackData data)
    {
        attackModifierSet.ApplyAttackModifier(data);
        modifyIncomingAttack(data);

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
