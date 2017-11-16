using UnityEngine;

public class TownUpgradeDialog : MonoBehaviour {
    public GameObject citizenInfluenceDescription;
    public GameObject politicalInfluenceDescription;
    public Transform optionParent;
    public TownUpgradeOptionUI optionPrefab;

    public void SetupCitizenInfluenceUpgrade(Town t)
    {
        politicalInfluenceDescription.gameObject.SetActive(false);
        citizenInfluenceDescription.gameObject.SetActive(true);
        SetupForTracks(t.citizensReputation.upgradeTracks, t);
    }

    public void SetupPoliticalInfluenceUpgrade(Town t)
    {
        citizenInfluenceDescription.gameObject.SetActive(false);
        politicalInfluenceDescription.gameObject.SetActive(true);
        SetupForTracks(t.politicalReputation.upgradeTracks, t);
    }

    private void SetupForTracks(TownUpgradeTracks upgradeTracks, Town t)
    {
        var upgrades = upgradeTracks.GetAvailableUpgrades();
        upgrades.ForEach(u => CreateOption(u, upgradeTracks, t));
    }

    private void CreateOption(TownUpgradeTracks.TrackToUpgrade u, TownUpgradeTracks upgradeTracks, Town town)
    {
        var optionGO = GameObject.Instantiate(optionPrefab.gameObject, optionParent) as GameObject;
        optionGO.GetComponent<TownUpgradeOptionUI>().Setup(u.option, () => SelectOption(u.track, upgradeTracks, town));
    }

    private void SelectOption(int track, TownUpgradeTracks upgradeTracks, Town town)
    {
        upgradeTracks.ActivateUpgrade(track, town);

        //TODO CLOSE!
        GameObject.Destroy(gameObject);
    }
}
