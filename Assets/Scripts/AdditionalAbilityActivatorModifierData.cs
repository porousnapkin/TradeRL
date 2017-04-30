public class AdditionalAbilityActivatorModifierData : AbilityModifierData
{
    public TargetedAnimationData animationData;
    public AbilityActivatorData activator;
    public AdditionalAbilityActivatorModifier.WhenToActivate whenToActivate;

    public override AbilityModifier Create(CombatController owner)
    {
        var modifier = DesertContext.StrangeNew<AdditionalAbilityActivatorModifier>();
        modifier.animation = animationData.Create(owner.character);
        modifier.additionalActivator = activator.Create(owner);
        modifier.whenToActivate = whenToActivate;
        return modifier;
    }
}

