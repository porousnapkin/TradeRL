using System.Collections.Generic;

public interface LabeledElement
{
    List<AbilityLabel> GetLabels();
}

public interface LabelRequiringElement
{
    List<AbilityLabel> GetLabelRestrictions();
}

public class ActiveLabelRequirements
{
    List<LabelRequiringElement> thingsWithRequirements = new List<LabelRequiringElement>();
    List<LabeledElement> thingsWithLabels = new List<LabeledElement>();

    public void AddRequirements(LabelRequiringElement thingWithRequirement)
    {
        thingsWithRequirements.Add(thingWithRequirement);
    }

    public void RemoveRequirements(LabelRequiringElement thingWithRequirement)
    {
        thingsWithRequirements.Remove(thingWithRequirement);
    }

    public void AddLabels(LabeledElement thingWithLabels)
    {
        thingsWithLabels.Add(thingWithLabels);
    }

    public void RemoveLabels(LabeledElement thingWithLabels)
    {
        thingsWithLabels.Remove(thingWithLabels);
    }

    public void ClearLabels()
    {
        thingsWithLabels.Clear();
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

    public List<AbilityLabel> GetActiveLabels()
    {
        var ret = new List<AbilityLabel>();
        thingsWithLabels.ForEach(t => ret.AddRange(t.GetLabels()));
        return ret;
    }
}

