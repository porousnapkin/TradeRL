using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class HealthDisplay : DesertView {
	public Text text;
    public Bar bar;

    public void Initialize(int currentHealth, int maxHealth)
    {
		text.text = "" + currentHealth + " / " + maxHealth;
        bar.SetInitialPercent((float)currentHealth / (float)maxHealth);
    }

	public void UpdateDisplay(int currentHealth, int maxHealth) {
		text.text = "" + currentHealth + " / " + maxHealth;
        bar.SetPercent((float)currentHealth / (float)maxHealth);
	}
}

public class HealthDisplayMediator : Mediator {
	[Inject] public HealthDisplay view { private get; set; }	
	[Inject] public Health model { private get; set; }

	public override void OnRegister()
    {
        view.Initialize(model.Value, model.MaxValue);
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