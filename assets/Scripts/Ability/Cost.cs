using UnityEngine;

public interface Cost {
	bool CanAfford();
	void PayCost();
    void Refund();

    void SetupVisualization(GameObject go);
}