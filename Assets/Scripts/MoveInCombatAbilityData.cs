public class MoveInCombatAbilityData : AbilityActivatorData
{
    public MoveInCombatAbility.WhereToMove whereToMove;
    public bool justMoveMe = false;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<MoveInCombatAbility>();

        a.controller = owner;
        a.whereToMove = whereToMove;
        a.justMoveMe = justMoveMe;

        return a;
    }
}