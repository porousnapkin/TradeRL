using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;

    [Inject] public TownEconomy economy { get; set; }
    [Inject] public TownPlayerBuildings playerBuildings {  get; set; }
    [Inject] public TownPlayerActions playerActions { get; set; }
    [Inject] public Reputation citizensReputation { get; set; }
    [Inject] public Reputation politicalReputation { get; set; }
    [Inject] public StatusEffects statusEffects { get; set; }
    [Inject] public RestModule restModule { get; set; }

    public List<ItemData> travelSuppliesAvailable = new List<ItemData>();
    public List<HireableAllyData> hireableAllies = new List<HireableAllyData>();
	public List<Town> rumoredLocations = new List<Town>();

	public void Setup(bool isCity) {
        economy.Setup(isCity, this);
        playerBuildings.Setup(this);
        playerActions.Setup(this);
        citizensReputation.Setup(this);
        politicalReputation.Setup(this);

        //Citizens reputation increases when buying and selling goods.
        economy.PlayerBoughtLocalGoods += citizensReputation.GainXP;
        economy.PlayerSoldForeignGoods += citizensReputation.GainXP;

        var basics = CityBasics.Instance;
        basics.defaultCityActivities.ForEach(a => playerActions.AddAction(a));
        travelSuppliesAvailable.AddRange(basics.defaultTravelSupplies);
        hireableAllies.AddRange(basics.hireableAllies);
	}
}