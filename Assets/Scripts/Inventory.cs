using UnityEngine;
using System.Collections.Generic;

public class TradeGood {
	public Town locationPurchased;	
	public int quantity = 0;
}

public class Inventory {
	List<TradeGood> goods = new List<TradeGood>();
	public List<TradeGood> Goods { get { return new List<TradeGood>(goods); }}
	public event System.Action GoodsChangedEvent = delegate{};

	int gold = 100;
	public int Gold { get { return gold; } set { gold = value; GoldChangedEvent(gold); }}
	public event System.Action<int> GoldChangedEvent = delegate{};

	public void GainTradeGood(Town locationPurchased, int quantity) {
		var good = goods.Find(g => g.locationPurchased == locationPurchased);
		if(good == null) {
			good = new TradeGood();
			good.locationPurchased = locationPurchased;
			goods.Add(good);
		}

		good.quantity += quantity;

		GoodsChangedEvent();
	}

	public void LoseTradeGood(Town locationPurchased, int quantity) {
		var good = goods.Find(g => g.locationPurchased == locationPurchased);
		if(good == null)
			throw new GoodNotFoundException();

		good.quantity -= quantity;
		if(good.quantity == 0)
			goods.Remove(good);
	}

	public class GoodNotFoundException : System.Exception {}
}
