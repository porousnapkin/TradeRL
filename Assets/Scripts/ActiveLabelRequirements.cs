using System.Collections.Generic;
using System.Linq;

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
        return requirements.TrueForAll(r => thingsWithLabels.Any(thingWithLabels => thingWithLabels.GetLabels().Contains(r)));
    }

    public List<AbilityLabel> GetActiveLabels()
    {
        var ret = new List<AbilityLabel>();
        thingsWithLabels.ForEach(t => ret.AddRange(t.GetLabels()));
        return ret;
    }
}

