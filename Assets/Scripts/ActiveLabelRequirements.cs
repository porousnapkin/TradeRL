using System.Collections.Generic;

public class ActiveLabelRequirements
{
    List<PlayerAbilityModifier> thingsWithRequirements = new List<PlayerAbilityModifier>();
    List<PlayerAbility> thingsWithLabels = new List<PlayerAbility>();

    public void AddRequirements(PlayerAbilityModifier thingWithRequirement)
    {
        thingsWithRequirements.Add(thingWithRequirement);
    }

    public void RemoveRequirements(PlayerAbilityModifier thingWithRequirement)
    {
        thingsWithRequirements.Remove(thingWithRequirement);
    }

    public void AddLabels(PlayerAbility thingWithLabels)
    {
        thingsWithLabels.Add(thingWithLabels);
    }

    public void RemoveLabels(PlayerAbility thingWithLabels)
    {
        thingsWithLabels.Remove(thingWithLabels);
    }

    public void ClearRequirements()
    {
        thingsWithRequirements.Clear();
    }

    public bool DoLabelsPassRequirements(List<AbilityLabel> labels)
    {
        return thingsWithRequirements.TrueForAll(t =>
            t.GetLabelRestrictions().TrueForAll(l =>
                labels.Contains(l)
            )
        );
    }

    public bool DoRequirementsMeetActiveLabels(List<AbilityLabel> requirements)
    {
        return thingsWithLabels.TrueForAll(t =>
            t.GetLabels().TrueForAll(l =>
                requirements.Contains(l)
            )
        );
    }
}

