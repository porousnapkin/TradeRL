using LitJson;
using UnityEngine;

public class MapViewData
{
    public class LocationViewData
    {
        public string tileDataName = "";
        public int baseTileIndex = 0;
        public int garnishTileIndex = 0;
    }
    public LocationViewData[,] viewData;
    MapCreationData mapCreationData;

    private void Setup(int width, int height)
    {
        mapCreationData = Resources.Load("MapData") as MapCreationData;
        mapCreationData.Setup();

        viewData = new LocationViewData[width, height];
    }

    public void Create(int width, int height, MapData mapData)
    {
        Setup(width, height);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                viewData[x, y] = CreateViewData(x, y, mapData, mapCreationData);
    }

    private LocationViewData CreateViewData(int x, int y, MapData mapData, MapCreationData mapCreationData)
    {
        if (mapData.IsCity(x, y))
            return CreateViewDataForSetTileData(mapCreationData.GetTileData("City"));
        else if (mapData.IsTown(x, y))
            return CreateViewDataForSetTileData(mapCreationData.GetTileData("Town"));
        else if (mapData.IsHill(x, y))
            return CreateViewDataForSetTileData(mapCreationData.GetTileData("Hill"));
        else
            return CreateViewDataForSetTileData(mapCreationData.GetTileData("Desert"));
    }

    private LocationViewData CreateViewDataForSetTileData(MapCreationData.SetTileData tileData)
    {
        var data = new LocationViewData();

        data.tileDataName = tileData.tileDataName;
        data.baseTileIndex = Random.Range(0, tileData.baseTiles.Count);
        if (Random.value < tileData.garnishChance)
            data.garnishTileIndex = Random.Range(0, tileData.garnishTiles.Count);
        else
            data.garnishTileIndex = -1;

        return data;
    }

    public LocationViewData CreateRandomDesertTile()
    {
        return CreateViewDataForSetTileData(mapCreationData.GetTileData("Desert"));
    }

    public Sprite GetBaseSprite(LocationViewData view)
    {
        return mapCreationData.GetBaseTileSprite(view.tileDataName, view.baseTileIndex);
    }

    public Sprite GetGarnishSprite(LocationViewData view)
    {
        return mapCreationData.GetGarnishTileSprite(view.tileDataName, view.garnishTileIndex);
    }

    public Sprite GetBaseSprite(int x, int y)
    {
        var view = viewData[x, y];
        return GetBaseSprite(view);
    }

    public Sprite GetGarnishSprite(int x, int y)
    {
        var view = viewData[x, y];
        return GetGarnishSprite(view);
    }

    public void Serialize(JsonWriter writer, int width, int height)
    {
        writer.WritePropertyName("MapCreatorView");
        writer.WriteArrayStart();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                writer.WriteObjectStart();

                var data = viewData[x, y];
                writer.WritePropertyName("tileDataName");
                writer.Write(data.tileDataName);
                writer.WritePropertyName("baseTileIndex");
                writer.Write(data.baseTileIndex);
                writer.WritePropertyName("garnishTileIndex");
                writer.Write(data.garnishTileIndex);

                writer.WriteObjectEnd();

            }
        }
        writer.WriteArrayEnd();
    }

    public void Deserialize(JsonReader reader, int width, int height)
    {
        Setup(width, height);
        reader.Read();//MapCreatorView property name
        int x = 0;
        int y = 0;
        reader.Read();//ArrayStart
        reader.Read(); //object start
        while (reader.Token != JsonToken.ArrayEnd)
        {
            viewData[x, y] = new LocationViewData();
            reader.Read(); //tileDataName property
            reader.Read(); //tileDataName value
            viewData[x, y].tileDataName = reader.Value.ToString();
            reader.Read(); //baseTileIndex property
            reader.Read(); //baseTileIndex value
            viewData[x, y].baseTileIndex = (int)reader.Value;
            reader.Read(); //garnishTileIndex property
            reader.Read(); //garnish value
            viewData[x, y].garnishTileIndex = (int)reader.Value;
            reader.Read(); //object end

            x++;
            if (x >= width)
            {
                x = 0;
                y++;
            }
            reader.Read(); //object start
        }
    }
}