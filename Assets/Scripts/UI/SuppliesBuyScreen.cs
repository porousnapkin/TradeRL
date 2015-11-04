using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuppliesBuyScreen : MonoBehaviour {
	public Button buy1Button;
	public Button buy10Button;
	public Button buySuggestedButton;
	public Text buy1Text;
	public Text buy10Text;
	public Text buySuggestedText;
	public Text suppliesText;
	public Text estimatedLengthText;
	public Text costText;
	public Button beginButton;
	public Button backButton;
	public GameObject nextScreen;
	public GameObject previousScreen;
	[HideInInspector]public Town myTown;
	[HideInInspector]public Town destinationTown;
	[HideInInspector]public Inventory inventory;
	int cost = 1;

	void Start() {
		SetupButtons ();
		inventory.GoldChangedEvent += GoldChanged;
		inventory.SuppliesChangedEvent += SuppliesChanged;
	}

	void OnEnable() {
		if(inventory != null)
			UpdateState ();
	}

	void OnDestroy() {
		inventory.GoldChangedEvent -= GoldChanged;
		inventory.SuppliesChangedEvent -= SuppliesChanged;
	}

	void SuppliesChanged(int supplies) {
		UpdateState();
	}

	void GoldChanged(int gold) {
		UpdateState();
	}
	
	void SetupButtons() {
		buy1Button.onClick.AddListener(() => Purchase (1));
		buy10Button.onClick.AddListener(() => Purchase(10));
		buySuggestedButton.onClick.AddListener(() => Purchase (GetSuggestedSupplies()));
		beginButton.onClick.AddListener(delegate() { gameObject.SetActive(false); nextScreen.SetActive(true); });
		backButton.onClick.AddListener(delegate() { gameObject.SetActive(false); previousScreen.SetActive(true); });
	}

	public void UpdateState() {
		UpdateText();
		UpdateButtons();
	}

	void UpdateText() {
		buy1Text.text = "Buy 1 (" + cost + " gold)";
		buy10Text.text = "Buy 10 (" + (10 * cost) + " gold)";
		buySuggestedText.text = "Buy " + GetSuggestedSupplies() + " (" + (GetSuggestedSupplies() * cost) + " gold)";
		suppliesText.text = "Current Supplies: " + inventory.Supplies.ToString();
		estimatedLengthText.text = "Estimated Expedition Length: " + GetEstimatedLength() + " days";
		costText.text = "Buy Supplies for " + cost.ToString() + " gold";
	}

	void UpdateButtons() {
		buy1Button.interactable = inventory.Gold >= cost;
		buy10Button.interactable = inventory.Gold >= cost * 10;
		buySuggestedButton.interactable = inventory.Gold >= cost * GetSuggestedSupplies() && GetSuggestedSupplies() > 0;
	}

	void Purchase(int amount) {
		inventory.Gold -= amount * cost;
		inventory.Supplies += amount;
	}

	int GetSuggestedSupplies() {
		return (int)Mathf.Max (GetEstimatedLength() - inventory.Supplies, 0);
	}

	int GetEstimatedLength() {
		return Mathf.RoundToInt(Vector2.Distance(myTown.worldPosition, destinationTown.worldPosition) * 1.25f);
	}
}
