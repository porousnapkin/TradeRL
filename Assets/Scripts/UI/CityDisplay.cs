using UnityEngine;

public class CityDisplay : MonoBehaviour {
	public Transform cityScenesParent;	
	[HideInInspector]public Town myTown;

	void Start() {
		var centerGO = CityActionFactory.CreateCityCenter(myTown);
		centerGO.transform.SetParent(cityScenesParent, false);
	}
}