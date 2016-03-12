using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using System.Linq;

#warning "GameBegan command should not have this many injections."
public class BeginGameCommand : EventCommand {
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
		LeanTween.delayedCall(0.0f, Run);
	}

	void Run() {
		#warning "These names suck. Also this is too much to setup, should be refactored and simplified I think..."
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
		cityDisplayGO.transform.SetParent (PrefabGetter.baseCanvas, false);

		#warning "Setting player health in game began command, which feels OBVIOUSLY wrong"
		playerCharacter.Setup(10);
		hiddenGrid.RevealSpotsNearPosition(mapPlayerController.position);
	}
}
