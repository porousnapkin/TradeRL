public class ChangeInitiativeAbilityData : AbilityActivatorData
{
    public int initiativeModifier;
    public string initiativeSource;
    public ChangeInitiativeAbility.TurnsAffected turnsAffected;
    public bool persisteNewInitiative = true;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<ChangeInitiativeAbility>();
        a.initiativeModifier = initiativeModifier;
        a.initiativeSource = initiativeSource;
        a.turnsAffected = turnsAffected;

        return a;
    }
}