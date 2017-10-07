using UnityEditor;
using UnityEngine;

public class MapCreatorWindow : EditorWindow
{
    public int numMapsToCreate;

    [MenuItem("Window/Map Creator")]
    static public void CreateTownTrait()
    {
        var window = EditorWindow.GetWindow<MapCreatorWindow>(true, "Map Creator", true);
        window.ShowPopup();
    }

    public void OnGUI()
    {
        GUILayout.Label("ScriptableObject Class");
        numMapsToCreate = EditorGUILayout.IntField("Num Maps To Create", numMapsToCreate);

        if (GUILayout.Button("Create"))
        {
            var mapData = new MapData();
            mapData.pathfinder = new DesertPathfinder();
            mapData.mapViewData = new MapViewData();
            mapData.Setup(new MapData.ViewData
            {
                width = 150,
                height = 150,
                minDistanceFromCities = 20,
                numCities = 7,
                minDistanceFromTowns = 20,
                numTowns = 12
            });
            mapData.CreateMap();

            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Maps/testMap.json", mapData.Serialize());
        }
    }
}
