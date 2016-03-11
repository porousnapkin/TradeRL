using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapData
{
	[Inject] public TownsAndCities townsAndCities { private get; set; }
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder { private get; set; }

	public class ViewData {
		public int width;
		public int height;
		public int minDistanceFromCities;
		public int numCities;
		public int minDistanceFromTowns;
		public int numTowns;
	}
	ViewData view;

	int[,] mapWeights;

	List<Vector2> cityLocations = new List<Vector2>();
	List<Vector2> townLocations = new List<Vector2>();

	CellularAutomata ca;

	public bool IsHill(Vector2 pos) { return ca.Graph[(int)pos.x, (int)pos.y]; }
	public bool IsCity(Vector2 pos) { return cityLocations.Contains(pos); }
	public bool IsTown(Vector2 pos) { return townLocations.Contains(pos); }
	public DesertPathfinder Pathfinder { get { return pathfinder; }}
	public bool CheckPosition(int x, int y) { return !(x >= view.width || x < 0 || y >= view.height || y < 0); }
	public int Width { get { return view.width; }}
	public int Height { get { return view.height; }}
	public class NoValidLocationFoundException : System.Exception {}
	
	public void Setup(ViewData viewData) {
		view = viewData;

		ca = new CellularAutomata(view.width, view.height);
		
		mapWeights = new int[view.width, view.height];
	}

	public void CreateMap () {
		//TODO: Remove magic numbers
		ca.BuildRandomCellularAutomataSet(5, 0.3f);
		
		CreateCityAndTownLocations();		

		SetupPathfindingWeights();
		pathfinder.SetMainMapWeights(mapWeights);
	}
	
	void CreateCityAndTownLocations() {
		try {
			CreateCityLocations();
			CreateTownLocations();
		} catch(NoValidLocationFoundException) {
			cityLocations.Clear();
			townLocations.Clear();
			
			Debug.LogWarning("Tried to make cities and locations and failed. Trying again. If this happens often, check town and city parameters.");
			CreateCityAndTownLocations();
		}

		var nameGenerator = new RandomNameGenerator();
		foreach(var location in cityLocations) 
			townsAndCities.AddCity(location, nameGenerator.GetCityName());
		foreach(var location in townLocations) 
			townsAndCities.AddTown(location, nameGenerator.GetTownName());
	}
	
	void CreateCityLocations() {
		for(int i = 0; i < view.numCities; i++) 
			cityLocations.Add(FindRandomCityLocation());
	}
	
	Vector2 FindRandomCityLocation() {
		Vector2 newLoc;
		bool validLoc = true;
		int attempts = 0;
		do {
			newLoc = new Vector2(Random.Range(1, view.width -1), Random.Range(1, view.height-1));
			validLoc = true;
			for(int i = 0; i < cityLocations.Count; i++) 
				if(Vector2.Distance(cityLocations[i], newLoc) < view.minDistanceFromCities)
					validLoc = false;
			
			attempts++;
			if(attempts > 30)
				throw new NoValidLocationFoundException();
		} while(!validLoc);
		
		return newLoc;
	}
	
	void CreateTownLocations() {
		for(int i = 0; i < view.numCities; i++) 
			townLocations.Add(FindRandomTownLocation());
	}
	
	Vector2 FindRandomTownLocation() {
		Vector2 newLoc;
		bool validLoc = true;
		int attempts = 0;
		do {
			newLoc = new Vector2(Random.Range(1, view.width -1), Random.Range(1, view.height-1));
			validLoc = true;
			for(int i = 0; i < cityLocations.Count; i++) 
				if(Vector2.Distance(cityLocations[i], newLoc) < view.minDistanceFromTowns)
					validLoc = false;
			for(int i = 0; i < townLocations.Count; i++)
				if(Vector2.Distance(townLocations[i], newLoc) < view.minDistanceFromTowns)
					validLoc = false;
			
			attempts++;
			if(attempts > 30)
				throw new NoValidLocationFoundException();
		} while(!validLoc);
		
		return newLoc;
	}

	void SetupPathfindingWeights() {
		for(int x = 0; x < view.width; x++) 
			for(int y = 0; y < view.height; y++) 
				SetupPathfindingWeight(x, y);
	}

	void SetupPathfindingWeight(int x, int y) {
		if(IsHill(new Vector2(x, y)))
			mapWeights[x,y] = SearchPoint.kImpassableWeight;
		else
			mapWeights[x,y] = 1;
	}
}
