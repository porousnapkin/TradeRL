using System.Collections.Generic;
using UnityEngine;

public class MapAbilityButtonsView : DesertView
{
    public GameObject abilityButtonPrefab;
    public GameObject confirmButtonPrefab;
	public event System.Action<PlayerActivatedPower> called = delegate { };
    ButtonArranger buttonArranger;
    List<AbilityButton> buttons = new List<AbilityButton>();

    protected override void Awake()
    {
        base.Awake();

        SetupButtonArranger();
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
        button.called += p => called(p);
		buttons.Add(button);
		buttonArranger.ArrangeButtons(buttons);
    }
}
