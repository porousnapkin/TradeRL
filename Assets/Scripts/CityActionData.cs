using UnityEngine;

public class CityActionData : ScriptableObject {
	public string actionDescription = "Find the local markets";
	public bool isCityCenter = false;
	public GameObject prefab;

	public GameObject Create(Town t) {
		DesertContext.QuickBind(t);
		var go = GameObject.Instantiate(prefab) as GameObject;
		DesertContext.FinishQuickBind<Town>();
		
		return go;
	}
}
