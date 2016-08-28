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

    void Start()
    {
        Invoke("TestSetup", 0.2f);
    }

    void Update()
    {
        buttons.ForEach(b => b.UpdateButtonStatus());
    }

    void TestSetup()
    {
        //TODO: Get player?
        AddButton(testMapAbilityData.Create(null));
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
    }
}

public class MapAbilityButtonsMediator : Mediator
{
    
}
