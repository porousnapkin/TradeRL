using UnityEngine;

public class TownUpgradeDialog : MonoBehaviour {
    public GameObject citizenInfluenceDescription;
    public GameObject politicalInfluenceDescription;
    public Transform optionParent;
    public TownUpgradeOptionUI optionPrefab;

    public void SetupCitizenInfluenceUpgrade(Town t)
    {
        citizenInfluenceDescription.gameObject.SetActive(true);

        var upgradeTracks = t.citizensReputation.upgradeTracks;
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
