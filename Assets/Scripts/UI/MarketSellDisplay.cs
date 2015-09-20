using UnityEngine;

public class MarketSellDisplay : MonoBehaviour {
	public Transform goodsDisplayParent;
	public GameObject goodsDisplayPrefab;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town myTown;

	void OnEnable() {
		foreach(Transform t in goodsDisplayParent)
			GameObject.Destroy(t.gameObject);

		var goods = inventory.PeekAtGoods();
		foreach(var g in goods)
			SetupGoods(g);
	}

	void SetupGoods(TradeGood g) {
		if(g.locationPurchased == myTown)
			return;
			
		var goodsGO = GameObject.Instantiate(goodsDisplayPrefab);
		goodsGO.transform.SetParent(goodsDisplayParent, false);
		var goodsDisplay = goodsGO.GetComponent<MarketSellTradeGoodDisplay>();

		goodsDisplay.tradeGood = g;
		goodsDisplay.inventory = inventory;
	}
}