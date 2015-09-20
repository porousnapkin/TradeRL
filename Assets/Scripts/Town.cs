using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;
	public List<CityAction> cityActions = new List<CityAction>();
}