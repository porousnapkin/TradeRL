public class TownGainsPoliticalReputationBenefit : TownBenefit
{
    public Town town { private get; set; }
    public int amount { private get; set; }

    public void Apply()
    {
        town.politicalReputation.GainXP(amount);
    }

    public void Undo()
    {
        //TODO: No way to lose XP. Do we want to be able to undo this?
    }
}

