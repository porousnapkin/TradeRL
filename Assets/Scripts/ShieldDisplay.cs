using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class ShieldDisplay : DesertView {
    public Text text;
    public Bar bar;

    int historicalMax = 0;

    public void Initialize(int currentShield)
    {
        if (currentShield == 0)
            HideShield();
        else
        {
            historicalMax = currentShield;
            text.text = "" + currentShield + " / " + historicalMax;
            bar.SetInitialPercent((float)currentShield / (float)historicalMax);
        }
    }

    public void UpdateDisplay(int currentShield)
    {
        if (currentShield <= 0 && historicalMax <= 0)
            HideShield();
        else
            ShowShield(currentShield);
    }

    private void ShowShield(int currentShield)
    {
        gameObject.SetActive(true);

        if (currentShield > historicalMax)
            historicalMax = currentShield;

        text.text = "" + currentShield + " / " + historicalMax;
        bar.SetPercent((float)currentShield / (float)historicalMax);
    }

    private void HideShield()
    {
        gameObject.SetActive(false);
        historicalMax = 0;
    }
}

public class ShieldDisplayMediator : Mediator
{
    [Inject]
    public ShieldDisplay view { private get; set; }
    [Inject]
    public Character character { private get; set; }
    private ShieldDefenseMod shield;

    public override void OnRegister()
    {
        shield = ShieldDefenseMod.GetFrom(character);

        ShieldChanged();
        shield.ShieldChangedEvent += ShieldChanged;
    }

    void ShieldChanged()
    {
        view.UpdateDisplay(shield.Value);
    }

    public override void OnRemove()
    {
        shield.ShieldChangedEvent -= ShieldChanged;
    }
}