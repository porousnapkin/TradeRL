using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
	public Text goldText;	
	public Text tradeGoodsText;
	[HideInInspector]public Inventory inventory;

	public void Setup() {
		inventory.GoldChangedEvent += SetGoldDisplay;
		SetGoldDisplay(inventory.Gold);

		inventory.GoodsChangedEvent += UpdateTradeGoodsDisplay;
		UpdateTradeGoodsDisplay();
	}

	void SetGoldDisplay(int gold) {
		goldText.text = "Gold: " + gold;
	}

	void UpdateTradeGoodsDisplay() {
		tradeGoodsText.text = "Trade Goods:";
		var goods = inventory.PeekAtGoods();
		foreach(var good in goods) 
			tradeGoodsText.text += "\n\n" + good.quantity + " goods from " + good.locationPurchased.name;
	}
}