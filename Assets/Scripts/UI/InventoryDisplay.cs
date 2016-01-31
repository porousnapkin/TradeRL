using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

public class InventoryDisplay : DesertView {
	public Text goldText;	
	public Text tradeGoodsText;
	public Text suppliesText;

	public void SetGoldDisplay(int gold) {
		goldText.text = "Gold: " + gold;
	}

	public void UpdateTradeGoodsDisplay(List<TradeGood> goods) {
		tradeGoodsText.text = "Trade Goods:";
		foreach(var good in goods) 
			tradeGoodsText.text += "\n\n" + good.quantity + " goods from " + good.locationPurchased.name;
	}

	public void SetSupplies(int supplies) {
		suppliesText.text = "Supplies: " + supplies;
	}
}

public class InventoryDisplayMediator : Mediator {
	[Inject] public InventoryDisplay display { private get; set; }
	[Inject] public Inventory inventory { private get; set; }

	public override void OnRegister ()
	{
		inventory.GoldChangedEvent += SetGoldDisplay;
		SetGoldDisplay();
		
		inventory.GoodsChangedEvent += UpdateTradeGoodsDisplay;
		UpdateTradeGoodsDisplay();
		
		inventory.SuppliesChangedEvent += SetSupplies;
		SetSupplies ();
	}

	public override void OnRemove ()
	{
		inventory.GoldChangedEvent -= SetGoldDisplay;
		inventory.GoodsChangedEvent -= UpdateTradeGoodsDisplay;
		inventory.SuppliesChangedEvent -= SetSupplies;
	}
	
	void SetGoldDisplay() {
		display.SetGoldDisplay(inventory.Gold);
	}
	
	void UpdateTradeGoodsDisplay() {
		display.UpdateTradeGoodsDisplay(inventory.PeekAtGoods());
	}
	
	void SetSupplies() {
		display.SetSupplies(inventory.Supplies);
	}
}
