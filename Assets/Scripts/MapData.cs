using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LitJson;

public class MapData
{
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder { private get; set; }
    [Inject] public MapViewData mapViewData { private get; set; }
    [Inject] public LocationMapData locationMapData { private get; set; }
    [Inject] public Towns towns { private get; set; }

	public class ViewData {
		public int width;
		public int height;
		public int minDistanceFromCities = 20;
		public int numCities = 20;
		public int minDistanceFromTowns = 20;
		public int numTowns = 0;
	}
	ViewData view;

	int[,] mapWeights;

	List<Vector2> cityLocations = new List<Vector2>();
	List<Vector2> townLocations = new List<Vector2>();

	CellularAutomata ca;
    int numCARuns = 8;
    float seedChanceForCAGrid = 0.4f;

    public bool IsImpassible(Vector2 pos) { return IsHill(pos); }
	public bool IsHill(Vector2 pos) { return ca.Graph[(int)pos.x, (int)pos.y] && !IsCity(pos); }
	public bool IsHill(int x, int y) { return ca.Graph[(int)x, (int)y] && !IsCity(x, y); }
    public bool IsCity(Vector2 pos) { return cityLocations.Any(l => l.x == pos.x && l.y == pos.y); }
    public bool IsCity(int x, int y) { return cityLocations.Any(l => l.x == x && l.y == y); }
    public bool IsTown(Vector2 pos) { return townLocations.Any(l => l.x == pos.x && l.y == pos.y); }
    public bool IsTown(int x, int y) { return townLocations.Any(l => l.x == x && l.y == y); }
	public DesertPathfinder Pathfinder { get { return pathfinder; }}
	public bool CheckPosition(int x, int y) { return !(x >= view.width || x < 0 || y >= view.height || y < 0); }
	public int Width { get { return view.width; }}
	public int Height { get { return view.height; }}
	public class NoValidLocationFoundException : System.Exception {}
	
	public void Setup(ViewData viewData) {
        if (ca != null)
            return;

		view = viewData;

        ca = new CellularAutomataDefault(view.width, view.height);
		
		mapWeights = new int[view.width, view.height];
	}

	public void CreateMap () {
		ca.BuildRandomCellularAutomataSet(numCARuns, seedChanceForCAGrid);
		
		CreateCityAndTownLocations();

        ConnectAllCities();

        mapViewData.Create(view.width, view.height, this);

        SetupTownAndCities(towns);

        locationMapData.CreateRandomLocations();
    }

    public void SetupPathfinding()
    {
		SetupPathfindingWeights();
		pathfinder.SetMainMapWeights(mapWeights);
    }

    public string Serialize()
    {
        var sb = new System.Text.StringBuilder();
        var writer = new JsonWriter(sb);

        writer.WriteObjectStart();

        writer.WritePropertyName("width");
        writer.Write(view.width);
        writer.WritePropertyName("height");
        writer.Write(view.height);

        writer.WritePropertyName("cities");
        writer.WriteArrayStart();
        foreach (var c in cityLocations)
            writer.Write(c.ToString());
        foreach (var c in townLocations)
            writer.Write(c.ToString());
        writer.WriteArrayEnd();

        locationMapData.Serialize(writer);
        mapViewData.Serialize(writer, Width, Height);

        writer.WritePropertyName("caVals");
        writer.WriteArrayStart();
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                writer.Write(ca.Graph[x, y]);
        writer.WriteArrayEnd();

        writer.WriteObjectEnd();

        return sb.ToString();
    }

    public void Deserialize(string jsonString)
    {
        JsonReader reader = new JsonReader(jsonString);

        reader.Read(); //object start
        reader.Read(); //width name
        reader.Read(); //width value
        view.width = (int)reader.Value;
        reader.Read(); //height name
        reader.Read(); //height value
        view.height = (int)reader.Value;

        //cities array
        reader.Read();//cities
        reader.Read();//ArrayStart
        reader.Read(); //string vector2
        while(reader.Token != JsonToken.ArrayEnd)
        {
            cityLocations.Add(StringToVector2((string)reader.Value));
            reader.Read(); //string vector2
        }

        locationMapData.Deserialize(reader);
        mapViewData.Deserialize(reader, Width, Height);

        //Cellular Automata array
        reader.Read();//caVals
        bool[,] graph = new bool[view.width,view.height];
        int x = 0;
        int y = 0;
        reader.Read();//ArrayStart
        reader.Read();//bool
        while(reader.Token != JsonToken.ArrayEnd)
        {
            graph[x, y] = (bool)reader.Value;
            x++;
            if(x >= view.width)
            {
                x = 0;
                y++;
            }
            reader.Read(); //bool
        }
        ca.Graph = graph;
    }

    public Vector2 StringToVector2(string rString)
    {
        string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        return new Vector2(x, y);
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
            return;
		}

        DebugLocationData();
	}

    public void SetupTownAndCities(Towns towns)
    {
        var nameGenerator = new RandomNameGenerator();
        foreach (var location in cityLocations)
            towns.AddCity(location, nameGenerator.GetCityName());
        foreach (var location in townLocations)
            towns.AddTown(location, nameGenerator.GetTownName());
    }

    List<Vector2> CitiesAndTowns()
    {
        List<Vector2> everything = new List<Vector2>();
        everything.AddRange(cityLocations);
        everything.AddRange(townLocations);
        return everything;
    }

    void DebugLocationData()
    {
        float avgDistance = 0;
        float biggestDistance = 0;
        float smallestDistance = float.MaxValue;
        int numAdded = 0;

        var everything = CitiesAndTowns();
        foreach(var a in everything)
        {
            float localSmallestDistance = float.MaxValue;

            foreach(var b in everything)
            {
                if (a == b)
                    continue;

                var dist = Vector2.Distance(a, b);
                localSmallestDistance = Mathf.Min(localSmallestDistance, dist);
            }

            avgDistance += localSmallestDistance;
            numAdded++;
            biggestDistance = Mathf.Max(biggestDistance, localSmallestDistance);
            smallestDistance = Mathf.Min(smallestDistance, localSmallestDistance);
        }
        avgDistance /= numAdded;

        Debug.Log("City Distance Data:");
        Debug.Log("Avg " + avgDistance);
        Debug.Log("min " + smallestDistance);
        Debug.Log("max " + biggestDistance);
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
			validLoc = IsHill(newLoc);

            if(validLoc)
    			for(int i = 0; i < cityLocations.Count; i++) 
    				if(Vector2.Distance(cityLocations[i], newLoc) < view.minDistanceFromCities)
    					validLoc = false;
			
			attempts++;
			if(attempts > 100)
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

    void ConnectAllCities()
    {
        for (int x = 0; x < view.width; x++)
        {
            for (int y = 0; y < view.height; y++)
            {
                if (IsHill(new Vector2(x, y)))
                    mapWeights[x, y] = 5;
                else
                    mapWeights[x, y] = 1;
            }
        }
		pathfinder.SetMainMapWeights(mapWeights);

        var everything = CitiesAndTowns();
        var everythingClone = new List<Vector2>(everything);
        everythingClone.Sort((a, b) => Random.Range(-2, 2));
        for(int i = 0; i < everything.Count; i++)
        {
            var a = everything[i];
            var b = everythingClone[i];

            if (a == b)
            {
                if (i < everythingClone.Count - 1)
                    b = everythingClone[i + 1];
                else
                    b = everything[0];
            }

            var path = pathfinder.SearchForPathOnMainMap(a, b);
            foreach (var point in path)
            {
                ca.Graph[(int)point.x, (int)point.y] = false;
            }
        }
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
