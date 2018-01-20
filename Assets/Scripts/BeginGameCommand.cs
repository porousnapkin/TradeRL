using UnityEngine;
using strange.extensions.command.impl;
using System.Linq;

//TODO: "GameBegan command should not need to have this many injections."
public class BeginGameCommand : EventCommand {
    public enum BeginType
    {
        FullGame,
        TestCombat, 
    }
    public static BeginType beginType;

	[Inject] public MapData mapData {private get; set; }
	[Inject] public MapCreator mapCreator {private get; set; }
	[Inject] public MapGraph mapGraph {private get; set; }
	[Inject] public Towns townsAndCities {private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set; }
	[Inject] public LocationFactory locationFactory {private get; set; }
	[Inject] public CityActionFactory cityActionFactory {private get; set;}
	[Inject] public TravelingStorySpawner spawner {private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
    [Inject] public Inventory inventory { private get; set; }

	public override void Execute()
	{
        switch(beginType)
        {
            case BeginType.FullGame:
		        LeanTween.delayedCall(0.0f, CreateMapAndPlacePlayer);
                break;
            case BeginType.TestCombat:
                break;
        }
	}

	void CreateMapAndPlacePlayer() {
        //TODO: "These class names suck. Also this is too much to setup, should be refactored and simplified I think..."
        //mapData.CreateMap();

        //TODO: THIS REALLY DOESNT WORK WITH BUILDS!!!!
        string text = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Maps/testMap.json");
        mapData.Deserialize(text);

        mapData.SetupPathfinding();
        mapData.SetupTownAndCities(townsAndCities);

		mapGraph.Setup ();
        hiddenGrid.Ready();
		
		townsAndCities.SetupTownEvents();

		var starterTown = townsAndCities.GetTownClosestToCenter();
		var sortedTowns = townsAndCities.GetTownsSortedByDistanceFromPoint (starterTown.worldPosition);
		townsAndCities.DiscoverLocation (sortedTowns [Random.Range (1, 4)]);
		var startPosition = starterTown.worldPosition;
		mapPlayerController.Teleport(startPosition);
		
		var sortedTAC = townsAndCities.GetTownsSortedByDistanceFromPoint (startPosition);
		sortedTAC.RemoveAll (t => t == starterTown);
		var destTown = sortedTAC.First ();
		townsAndCities.DiscoverLocation(destTown);
		
		locationFactory.CreateLocations();
		
		var cityDisplayGO = cityActionFactory.CreateDisplayForCity (starterTown);

		hiddenGrid.RevealSpotsNearPosition(mapPlayerController.position);

        //TODO: This should be temp.
        //inventory.AddItem(BasePlayerCharacterStats.Instance.debugItem.Create(null));

        BasePlayerCharacterStats.Instance.premadeToUse.Setup();
	}
}
