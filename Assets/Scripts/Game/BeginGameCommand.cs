using UnityEngine;
using System.Collections;
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
	[Inject] public TownsAndCities townsAndCities {private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set; }
	[Inject] public LocationFactory locationFactory {private get; set; }
	[Inject] public CityActionFactory cityActionFactory {private get; set;}
	[Inject] public TravelingStorySpawner spawner {private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
	[Inject (Character.PLAYER)] public Character playerCharacter { private get; set;}

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
		mapData.CreateMap();
		mapGraph.Setup ();
		mapCreator.CreateMap();
		
		townsAndCities.SetupCityAndTownEvents();
		townsAndCities.Setup(); //This function is totally superfluous if you look inside it....

		spawner.Setup();
		
		var starterTown = townsAndCities.GetTownFurthestFromCities ();
		var sortedTowns = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (starterTown.worldPosition);
		townsAndCities.DiscoverLocation (sortedTowns [Random.Range (1, 4)]);
		var startPosition = starterTown.worldPosition;
		mapPlayerController.Teleport(startPosition);
		
		var sortedTAC = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (startPosition);
		sortedTAC.RemoveAll (t => t == starterTown);
		var destTown = sortedTAC.First ();
		townsAndCities.DiscoverLocation(destTown);
		
		locationFactory.CreateLocations();
		
		var cityDisplayGO = cityActionFactory.CreateDisplayForCity (starterTown);
        cityDisplayGO.GetComponentInChildren<TownDialog>().SimulateButtonHitForAction(TownDialog.cheatExpeditionName);

		//TODO: "Setting player health in game began command, which feels OBVIOUSLY wrong"
		playerCharacter.Setup(10);
		hiddenGrid.RevealSpotsNearPosition(mapPlayerController.position);
	}
}
