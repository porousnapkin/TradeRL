public class MoveInCombatAbilityData : AbilityActivatorData
{
    public MoveInCombatAbility.WhereToMove whereToMove;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<MoveInCombatAbility>();

        a.controller = owner;
        a.whereToMove = whereToMove;

        return a;
    }
}