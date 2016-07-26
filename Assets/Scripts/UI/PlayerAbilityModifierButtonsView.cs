﻿using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class PlayerAbilityModifierButtonsView : DesertView
{
	public GameObject buttonPrefab;
	public event System.Action<PlayerAbilityModifier> called;
	ButtonArranger buttonArranger;
	List<AbilityButton> buttons = new List<AbilityButton>();

	void Awake() 
	{
		buttonArranger = new ButtonArranger();
		buttonArranger.buttonPrefab = buttonPrefab;
		buttonArranger.parentTransform = transform;
	}

	public void AddButton(PlayerAbilityModifier ability) 
	{
		buttons.Add(buttonArranger.CreateButton(ability, ButtonHit));
		buttonArranger.ArrangeButtons(buttons);
	}

	void ButtonHit(PlayerActivatedPower ability)
	{
		//This feels hacky. Better way to do this?
		called(ability as PlayerAbilityModifier);
	}

	public void ShowButtons()
	{
		buttons.ForEach(b => b.gameObject.SetActive(true));
	}

	public void HideButtons()
	{
		buttons.ForEach(b => b.gameObject.SetActive(false));
	}

	public void RemoveAllButtons() {
		buttons.ForEach(b => buttons.Remove(b));
		buttonArranger.ArrangeButtons(buttons);
	}
}

public class PlayerAbilityModifierButtonsMediator : Mediator
{
	[Inject] public PlayerAbilityModifierButtonsView view { private get; set; }
	[Inject] public PlayerAbilityModifierButtonsMediated model { private get; set; }

	public override void OnRegister ()
	{
		model.buttonAdded += view.AddButton;
		model.buttonsShown += view.ShowButtons;
		model.buttonsHid += view.HideButtons;
		model.allButtonsRemoved += view.RemoveAllButtons;
		view.called += model.ButtonHit;
	}

	public override void OnRemove()
	{
		model.buttonAdded -= view.AddButton;
		model.buttonsShown -= view.ShowButtons;
		model.buttonsHid -= view.HideButtons;
		model.allButtonsRemoved -= view.RemoveAllButtons;
		view.called -= model.ButtonHit;
	}
}

public interface PlayerAbilityModifierButtons
{
	event System.Action<PlayerAbilityModifier> buttonActivatedEvent;
	event System.Action<PlayerAbilityModifier> buttonDeactivatedEvent;
	void Setup(List<PlayerAbilityModifier> modifiers);
	void Show();
	void Hide();
}

public interface PlayerAbilityModifierButtonsMediated
{
	event System.Action<PlayerAbilityModifier> buttonAdded;
	event System.Action buttonsShown;
	event System.Action buttonsHid;
	event System.Action allButtonsRemoved;
	void ButtonHit(PlayerAbilityModifier ability);
}

public class AbilityModifierButtonsImpl : PlayerAbilityModifierButtons, PlayerAbilityModifierButtonsMediated
{
	public event System.Action<PlayerAbilityModifier> buttonActivatedEvent = delegate {};
	//Need to call this too...
	public event System.Action<PlayerAbilityModifier> buttonDeactivatedEvent = delegate {};
	public event System.Action<PlayerAbilityModifier> buttonAdded = delegate{};
	public event System.Action buttonsShown = delegate{};
	public event System.Action buttonsHid = delegate{};
	public event System.Action allButtonsRemoved = delegate{};

	public void Setup(List<PlayerAbilityModifier> modifiers)
	{
		allButtonsRemoved();
		modifiers.ForEach(m => buttonAdded(m));
		buttonsHid();
	}

	public void Show() 
	{
		buttonsShown();
	}

	public void Hide()
	{
		buttonsHid();
	}

	public void ButtonHit(PlayerAbilityModifier modifier) 
	{
		buttonActivatedEvent(modifier);
	}
}

