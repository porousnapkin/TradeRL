using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class MapAbilityButtonsView : DesertView
{
    public GameObject abilityButtonPrefab;
	public event System.Action<PlayerActivatedPower> called = delegate { };
    ButtonArranger buttonArranger;
    List<AbilityButton> buttons = new List<AbilityButton>();

    public MapAbilityData testMapAbilityData;

    protected override void Awake()
    {
        base.Awake();

        SetupButtonArranger();
    }

    void Update()
    {
        buttons.ForEach(b => b.UpdateButtonStatus());
    }

    void SetupButtonArranger()
    {
        buttonArranger = new ButtonArranger();
        buttonArranger.buttonPrefab = abilityButtonPrefab;
        buttonArranger.parentTransform = transform;
    }

    public void AddButton(PlayerActivatedPower power)
    {
        var button = buttonArranger.CreateButton(power);
        button.called += ButtonHit;
		buttons.Add(button);
		buttonArranger.ArrangeButtons(buttons);
    }

    public void ButtonHit(PlayerActivatedPower power)
    {
        //TODO: should this be activated externally?
        power.Activate(() => { });
        called(power);
        power.PrePurchase();
    }

    public void RemoveButton(PlayerActivatedPower power)
    {
        var button = buttons.Find(b => b.IsAbilityForThisPower(power));
        if (button == null)
            return;

        buttons.Remove(button);
        GameObject.Destroy(button.gameObject);
        buttonArranger.ArrangeButtons(buttons);
    }
}

public class MapAbilityButtonsMediator : Mediator
{
    [Inject] public MapAbilityButtonsMediated model {private get; set; }
    [Inject] public MapAbilityButtonsView view { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        model.addingButton += view.AddButton;
        model.removingButton += view.RemoveButton;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        model.addingButton -= view.AddButton;
        model.removingButton -= view.RemoveButton;
    }
}

public interface MapAbilityButtonsMediated
{
    event Action<PlayerActivatedPower> addingButton;
    event Action<PlayerActivatedPower> removingButton;
}

public interface MapAbilityButtons
{
    void AddButton(PlayerActivatedPower power);
    void RemoveButton(PlayerActivatedPower power);
}

public class MapAbilityButtonsImpl : MapAbilityButtons, MapAbilityButtonsMediated
{
    public event Action<PlayerActivatedPower> addingButton;
    public event Action<PlayerActivatedPower> removingButton;

    public void AddButton(PlayerActivatedPower power)
    {
        addingButton(power);
    }

    public void RemoveButton(PlayerActivatedPower power)
    {
        removingButton(power);
    }
}
