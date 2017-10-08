using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LitJson;

public class LocationMapData
{
    [Inject] public MapData mapData { private get; set; }
    public event System.Action<int, int> locationAdded = delegate { };
    public event System.Action<int, int> locationRemoved = delegate { };

    public class LocationDataOnMap
    {
        public LocationData locationData;
        public int x;
        public int y;
    }

    public List<LocationDataOnMap> locationDataOnMap = new List<LocationDataOnMap>();
    HashSet<Vector2> usedPositions = new HashSet<Vector2>();
    List<LocationData> locationDataList;

    const int numLocations = 150;

    private void Setup()
    {
        var locationDatas = Resources.LoadAll<LocationData>("Locations");
        locationDataList = locationDatas.ToList();
        locationDataList.RemoveAll(l => !l.randomlyPlace);
    }

    public void CreateRandomLocations()
    {
        Setup();
        usedPositions.Clear();
        var randomLocationList = new List<LocationData>(locationDataList);
        randomLocationList.RemoveAll(l => !l.randomlyPlace);

        for (int i = 0; i < numLocations; i++)
            SetupLocation(randomLocationList[Random.Range(0, randomLocationList.Count)]);
    }

    void SetupLocation(LocationData loc)
    {
        var pos = GetAvailablePosition();

        AddLocationPositionData(loc, (int)pos.x, (int)pos.y);
    }

    void AddLocationPositionData(LocationData loc, int x, int y)
    {
        var l = new LocationDataOnMap();
        l.x = x;
        l.y = y;
        l.locationData = loc;

        locationDataOnMap.Add(l);

        usedPositions.Add(new Vector2(x, y));
        //Add adjacent positions as well so we don't place locations next to each other.
        for (int xAdd = -1; xAdd <= 1; xAdd++)
            for (int yAdd = -1; yAdd <= 1; yAdd++)
                if (x != 0 || y != 0)
                    usedPositions.Add(new Vector2(x + xAdd, y + yAdd));
    }

    Vector2 GetAvailablePosition()
    {
        var randomPos = new Vector2(Random.Range(0, mapData.Width), Random.Range(0, mapData.Height));

        if (mapData.IsCity(randomPos) || mapData.IsTown(randomPos))
            return GetAvailablePosition();
        if (mapData.IsHill(randomPos))
            return GetAvailablePosition();
        if (usedPositions.Contains(randomPos))
            return GetAvailablePosition();

        return randomPos;
    }

    public void AddLocationToPosition(LocationData loc, int x, int y)
    {
        AddLocationPositionData(loc, x, y);
        locationAdded(x, y);
    }

    public void RemoveLocationFromPosition(int x, int y)
    {
        locationDataOnMap.RemoveAll(a => a.x == x && a.y == y);
        locationRemoved(x, y);
    }

    public LocationData GetLocationData(int x, int y)
    {
        var onMapData = locationDataOnMap.Find(a => a.x == x && a.y == y);
        if (onMapData == null)
            return null;

        return onMapData.locationData;
    }

    public void Serialize(JsonWriter writer)
    {
        writer.WritePropertyName("Locations");
        writer.WriteArrayStart();
        foreach (var loc in locationDataOnMap)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("DataName");
            writer.Write(loc.locationData.name);
            writer.WritePropertyName("x");
            writer.Write(loc.x);
            writer.WritePropertyName("y");
            writer.Write(loc.y);
            writer.WriteObjectEnd();
        }
        writer.WriteArrayEnd();
    }

    public void Deserialize(JsonReader reader)
    {
        Setup();
        reader.Read();//Locations property name
        reader.Read();//ArrayStart
        reader.Read(); //object start
        while (reader.Token != JsonToken.ArrayEnd)
        {
            reader.Read(); //dataName property
            reader.Read(); //dataName value
            var name = reader.Value.ToString();
            var locationData = locationDataList.Find(d => d.name == name);
            reader.Read(); //x property
            reader.Read(); //x value
            var x = (int)reader.Value;
            reader.Read(); //y property
            reader.Read(); //y value
            var y = (int)reader.Value;
            reader.Read(); //object end
            reader.Read(); //object start
            AddLocationPositionData(locationData, x, y);
        }
    }
}

public class LocationFactory {
    [Inject] public LocationMapData locationMapData { private get; set; }

	List<Location> locations = new List<Location>();
	const int numLocations = 150;
	
	public void CreateLocations() {
        foreach(var d in locationMapData.locationDataOnMap)
            CreateLocationAtPosition(d.locationData, d.x, d.y);
	}

    public void AddLocationToPosition(LocationData loc, int x, int y)
    {
        CreateLocationAtPosition(loc, x, y);
        locationMapData.AddLocationToPosition(loc, x, y);
    }

    void CreateLocationAtPosition(LocationData loc, int x, int y)
    {
        var l = DesertContext.StrangeNew<Location>();
		l.x = x;
		l.y = y;
		l.data = loc;
        l.description = "<u>" + loc.locationName + "</u>";
		l.Setup();
		locations.Add(l);
    }
}
