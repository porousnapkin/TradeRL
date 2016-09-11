using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class HealthDisplay : DesertView {
	public Text text;	

	public void UpdateDisplay(int currentHealth, int maxHealth) {
		text.text = "HP: " + currentHealth + " / " + maxHealth;
	}
}

public class HealthDisplayMediator : Mediator {
	[Inject] public HealthDisplay view { private get; set; }	
	[Inject] public Health model { private get; set; }

	public override void OnRegister()
    {
        HealthChanged();
    	model.HealthChangedEvent += HealthChanged;
    }

    void HealthChanged() {
    	view.UpdateDisplay(model.Value, model.MaxValue);
    }

	public override void OnRemove()
    {
    	model.HealthChangedEvent -= HealthChanged;
    }
}