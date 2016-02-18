using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using System.Linq;

#warning "GameBegan command should not have this many injections."
public class GameBeganCommand : EventCommand {
	[Inject] public MapData mapData {private get; set; }
	[Inject] public MapCreator mapCreator {private get; set; }
	[Inject] public MapGraph mapGraph {private get; set; }
	[Inject] public TownsAndCities townsAndCities {private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set; }
	[Inject] public LocationFactory locationFactory {private get; set; }
	[Inject] public CityActionFactory cityActionFactory {private get; set;}
	[Inject (Character.PLAYER)] public Character playerCharacter { private get; set;}

	public override void Execute()
	{
		LeanTween.delayedCall(0.0f, Run);
	}

	void Run() {
		Debug.Log ("Run!");
		
		#warning "These names suck. Also this is too much to setup, should be refactored and simplified I think..."
		mapData.CreateMap();
		mapGraph.Setup ();
		mapCreator.CreateMap();
		
		townsAndCities.SetupCityAndTownEvents();
		townsAndCities.Setup(); //This function is totally superfluous if you look inside it....
		
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
		playerCharacter.Setup(100);

		Resources.Load<TravelingStoryData>("TravelingStory/HyenaAttack").Create(startPosition + new Vector2(0, 2));
	}

	/*void SetupTownsAndCitiesObject ()
	{
		Debug.Log ("Getting towns and cities");
		townsAndCities = mapCreator.GetTownsAndCities ();
		townsAndCities.SetupCityAndTownEvents (mapGraph, canvasParent);
		FactoryRegister.SetTownsAndCities (townsAndCities);
		townsAndCities.Setup (gameDate);
	}
	
	void SetupStartCity ()
	{
		starterTown = townsAndCities.GetTownFurthestFromCities ();
		var sortedTowns = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (starterTown.worldPosition);
		townsAndCities.DiscoverLocation (sortedTowns [Random.Range (1, 4)]);
		startPosition = starterTown.worldPosition;
		mapGraph.SetCharacterToPosition (startPosition, startPosition, playerCharacter);
	}
	
	void SetupFirstTradeDestination ()
	{
		var sortedTAC = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (startPosition);
		sortedTAC.RemoveAll (t => t == starterTown);
		var destLoc = sortedTAC.First ().worldPosition;
		destination.destinationPosition = destLoc;
	}
	
	void CreateTownUIForStartTown ()
	{
		var cityDisplayGO = CityActionFactory.CreateDisplayForCity (starterTown);
		cityDisplayGO.transform.SetParent (canvasParent, false);
	}*/
}
