using strange.extensions.mediation.impl;
using UnityEngine;

public class TravelSuppliesDisplay : CityActionDisplay
{
	public Transform suppliesParent;
	public GameObject suppliesOptionPrefab;
	Town town;
	Inventory inventory;
    PlayerCharacter playerCharacter;

    public void Setup(Town town, Inventory inventory, PlayerCharacter playerCharacter)
    {
        this.town = town;
        this.inventory = inventory;
        this.playerCharacter = playerCharacter;

        SetupSupplies();
    }

    private void SetupSupplies()
    {
        var travelSupplies = town.travelSuppliesAvailable;
        travelSupplies.ForEach(t => SetupTravelSupply(t));
    }

    private void SetupTravelSupply(ItemData item)
    {
        var optionGO = GameObject.Instantiate(suppliesOptionPrefab, suppliesParent);
        var option = optionGO.GetComponent<TravelSupplyOption>();
        option.Setup(item, inventory, playerCharacter);
    }
}

public class TravelSuppliesDisplayMediator : Mediator
{
    [Inject] public Town town { private get; set; }
    [Inject] public Inventory inventory { private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
    [Inject] public TravelSuppliesDisplay view { private get; set; }

    public override void OnRegister()
    {
        view.Setup(town, inventory, playerCharacter);
    }
}

