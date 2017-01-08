public class AdditionalAbilityActivatorModifierData : AbilityModifierData
{
    public TargetedAnimationData animationData;
    public AbilityActivatorData activator;

    public override AbilityModifier Create(CombatController owner)
    {
        var modifier = DesertContext.StrangeNew<AdditionalAbilityActivatorModifier>();
        modifier.animation = animationData.Create(owner.character);
        modifier.additionalActivator = activator.Create(owner);
        return modifier;
    }
}

