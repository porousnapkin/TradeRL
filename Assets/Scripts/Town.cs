using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;

    public TownEconomy economy = new TownEconomy();
    public TownPlayerBuildings playerBuildings = new TownPlayerBuildings();
    public TownPlayerActions playerActions = new TownPlayerActions();

    public List<ItemData> travelSuppliesAvailable = new List<ItemData>();
    public List<HireableAllyData> hireableAllies = new List<HireableAllyData>();
	public List<Town> rumoredLocations = new List<Town>();

	public void Setup(GameDate gameDate, bool isCity) {
        economy.Setup(gameDate, isCity, this);
        playerBuildings.Setup(this);
        playerActions.Setup(this);
	}
}