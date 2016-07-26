﻿using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class PlayerAbilityModifierButtonsView : DesertView
{
	public GameObject buttonPrefab;
	public event System.Action<PlayerAbilityModifier> modifierSelected;
	public event System.Action<PlayerAbilityModifier> modifierUnselected;
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
		var button = buttonArranger.CreateButton(ability);
		button.called += (a) => ButtonHit(button, a);
		buttons.Add(button);
		buttonArranger.ArrangeButtons(buttons);
	}

	void ButtonHit(AbilityButton button, PlayerActivatedPower ability)
	{
		button.ToggleSelected();

		//This feels hacky. Better way to do this?
		var modifier = ability as PlayerAbilityModifier;

		if(button.IsSelected())
			modifierSelected(modifier);
		else
			modifierUnselected(modifier);

		buttons.ForEach(b => b.UpdateButtonStatus());
	}

	public void ShowButtons()
	{
		buttons.ForEach(b => {
			b.gameObject.SetActive(true);
			b.SetUnselected();
			b.UpdateButtonStatus();
		});
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
		view.modifierSelected += model.ModifierSelected;
		view.modifierUnselected += model.ModifierUnselected;
	}

	public override void OnRemove()
	{
		model.buttonAdded -= view.AddButton;
		model.buttonsShown -= view.ShowButtons;
		model.buttonsHid -= view.HideButtons;
		model.allButtonsRemoved -= view.RemoveAllButtons;
		view.modifierSelected -= model.ModifierSelected;
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
	void ModifierSelected(PlayerAbilityModifier ability);
	void ModifierUnselected(PlayerAbilityModifier ability);
}

public class AbilityModifierButtonsImpl : PlayerAbilityModifierButtons, PlayerAbilityModifierButtonsMediated
{
	public event System.Action<PlayerAbilityModifier> buttonActivatedEvent = delegate {};
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

	public void ModifierSelected(PlayerAbilityModifier modifier) 
	{
		buttonActivatedEvent(modifier);
	}

	public void ModifierUnselected(PlayerAbilityModifier modifier) 
	{
		buttonDeactivatedEvent(modifier);
	}
}
