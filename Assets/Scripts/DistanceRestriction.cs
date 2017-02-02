public class DistanceRestriction : Restriction {
    public enum DistanceType
    {
        MustBeInMelee,
        MustBeAtRange,
    }
    public DistanceType type { private get; set; }
    [Inject] public ActiveLabelRequirements activeLabels { private get; set; }
    public Character character;

    public bool CanUse()
    {
        switch(type)
        {
            case DistanceType.MustBeInMelee:
                return character.IsInMelee || activeLabels.GetActiveLabels().Contains(AbilityLabel.MovesToMelee);
            case DistanceType.MustBeAtRange:
                return !character.IsInMelee || activeLabels.GetActiveLabels().Contains(AbilityLabel.MovesToRanged);
        }

        return true;
    }
}
