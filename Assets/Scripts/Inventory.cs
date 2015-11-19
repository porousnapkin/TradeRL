using UnityEngine;
using System.Collections.Generic;

public class TradeGood {
	public Town locationPurchased;	
	public int quantity = 0;
	public int purchasePrice = 10;
}

public class Inventory {
	//DEBUG TEMP
	public static Inventory Instance;
	public Inventory() { Instance = this; }

	List<TradeGood> goods = new List<TradeGood>();
	public event System.Action GoodsChangedEvent = delegate{};
	public static int maxGoodsPerCamel = 10;
	int camels = 1;
	public int MaxGoodsCapacity { 
		get { return camels * maxGoodsPerCamel; } 
	}
	public int Camels {
		get { return camels; }
	}
	public void AddACamel(int numToAdd) {
		camels += numToAdd; 
		MaxGoodsCapacityChangedEvent();
	}
	public int RemainingGoodsSpace {
		get { return MaxGoodsCapacity - GetNumGoods(); }
	}
	public event System.Action MaxGoodsCapacityChangedEvent = delegate{};

	int gold = 100;
	public int Gold { get { return gold; } set { gold = value; GoldChangedEvent(gold); }}
	public event System.Action<int> GoldChangedEvent = delegate{};

	int supplies = 0;
	public int Supplies { get { return supplies; } set { supplies = value; SuppliesChangedEvent(supplies); }}
	public event System.Action<int> SuppliesChangedEvent = delegate{};

	public List<TradeGood> PeekAtGoods() {  
		return new List<TradeGood>(goods); 
	}

	public int GetNumGoods() {
		int amount = 0;
		foreach(var g in goods)
			amount += g.quantity;

		return amount;
	}

	public void GainTradeGood(Town locationPurchased, int quantity, int purchasePrice) {
		var good = goods.Find(g => g.locationPurchased == locationPurchased);
		if(good == null) {
			good = new TradeGood();
			good.locationPurchased = locationPurchased;
			good.purchasePrice = purchasePrice;
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

		GoodsChangedEvent();
	}

	public class GoodNotFoundException : System.Exception {}
}
