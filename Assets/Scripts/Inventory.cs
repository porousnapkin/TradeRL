using UnityEngine;
using System.Collections.Generic;

public class TradeGood {
	public Town locationPurchased;	
	public int quantity = 0;
	public int purchasePrice = 10;
}

public class Inventory {
	List<TradeGood> goods = new List<TradeGood>();
	public event System.Action GoodsChangedEvent = delegate{};

	List<Item> items = new List<Item>();
	public int baseJamSaves = 0;

	int gold = 45;
	public int Gold { get { return gold; } set { gold = value; GoldChangedEvent(); }}
	public event System.Action GoldChangedEvent = delegate{};

    public void AddItem(Item item)
    {
        var sameItem = GetItemByName(item.GetName());
        if (sameItem != null)
            sameItem.SetNumItems(sameItem.GetNumItems() + item.GetNumItems());
		else {
			item.SetBaseJamSaves(baseJamSaves);
            items.Add(item);
            item.SetNumItems(1);
		}
    }

    public Item GetItemByName(string name)
    {
        return items.Find(i => i.GetName() == name);
    }

    public int GetNumItemsByName(string name)
    {
        var item = GetItemByName(name);
        if (item == null)
            return 0;
        return item.GetNumItems();
    }

	public List<Item> GetJammedItems()
	{
		return items.FindAll(i => i.IsJammed());
	}

    public List<Item> GetItemsWithReducedJamChecks()
    {
        return items.FindAll(i => i.GetRemainingJamSaves() < i.GetTotalJamSaves());
    }

    public List<Item> GetItems()
    {
        return items;
    }

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
			goods.Add(good);
		}

		good.quantity += quantity;
		good.purchasePrice = purchasePrice;

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

	public int GetBaseJamSaves() 
	{
		return baseJamSaves;
	}

	public void SetBaseJamSaves(int value) 
	{
		baseJamSaves = value;
		items.ForEach(i => i.SetBaseJamSaves(baseJamSaves));
	}
}