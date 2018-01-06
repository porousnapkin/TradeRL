using System.Collections.Generic;

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

    public void CreateALocationNearAPoint(LocationData loc, int x, int y, int minRange, int maxRange)
    {
        var pos = locationMapData.GetAvailablePositionNearPoint(x, y, minRange, maxRange);
        AddLocationToPosition(loc, (int)pos.x, (int)pos.y);
    }
}
