using System.Collections.Generic;

public class TownUpgradeTracks
{
    List<int> optionLevels = new List<int>();
    List<List<TownUpgradeOptionData>> optionTracks = new List<List<TownUpgradeOptionData>>();

    public class TrackToUpgrade
    {
        public int track = 0;
        public TownUpgradeOptionData option;
    }

    public void Setup(List<TownData.ListOfTownUpgradeOptions> tracks)
    {
        tracks.ForEach(t =>
        {
            optionTracks.Add(t.list);
            optionLevels.Add(0);
        });
    }

    public List<TrackToUpgrade> GetAvailableUpgrades()
    {
        var upgrades = new List<TrackToUpgrade>();

        for(int i = 0; i < optionTracks.Count; i++)
        {
            if (optionLevels[i] < optionTracks[i].Count)
                upgrades.Add(new TrackToUpgrade {
                    track = i,
                    option = optionTracks[i][optionLevels[i]]
                });
        }

        return upgrades;
    }

    public void ActivateUpgrade(int trackIndex, Town t)
    {
        optionTracks[trackIndex][optionLevels[trackIndex]].Apply(t);
        optionLevels[trackIndex]++;
    }
}
