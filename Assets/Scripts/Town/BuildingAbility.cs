using UnityEngine;
using System.Collections;

public interface BuildingAbility {
	void Build();
	void ActivateBuilt();
	string DescribeUnbuilt();
	string DescribeBuilt();
}
