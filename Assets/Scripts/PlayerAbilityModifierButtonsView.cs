using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class PlayerAbilityModifierButtonsView : DesertView
{
	public GameObject buttonPrefab;
	public event System.Action<PlayerAbilityModifier> modifierSelected;
	public event System.Action<PlayerAbilityModifier> modifierUnselected;
	ButtonArranger buttonArranger;
	List<AbilityButton> buttons = new List<AbilityButton>();

	protected override void Awake() 
	{
        base.Awake();

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
            if (!b.IsSelected())
                b.SetUnselected();
            else
                b.SetSelected();
			b.UpdateButtonStatus();
		});
	}

    public void UpdateButtonStatus()
    {
		buttons.ForEach(b => b.UpdateButtonStatus());
    }

	public void HideButtons()
	{
        buttons.ForEach(b => b.SetUnselected());
		buttons.ForEach(b => b.gameObject.SetActive(false));
	}

	public void RemoveAllButtons() {
		buttons.ForEach(b => buttons.Remove(b));
        buttons.Clear();
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
        model.updateButtonStatus += view.UpdateButtonStatus;
		view.modifierSelected += model.ModifierSelected;
		view.modifierUnselected += model.ModifierUnselected;
	}

	public override void OnRemove()
	{
		model.buttonAdded -= view.AddButton;
		model.buttonsShown -= view.ShowButtons;
		model.buttonsHid -= view.HideButtons;
		model.allButtonsRemoved -= view.RemoveAllButtons;
        model.updateButtonStatus -= view.UpdateButtonStatus;
		view.modifierSelected -= model.ModifierSelected;
		view.modifierUnselected -= model.ModifierUnselected;
	}
}

public interface PlayerAbilityModifierButtons
{
	event System.Action<PlayerAbilityModifier> buttonActivatedEvent;
	event System.Action<PlayerAbilityModifier> buttonDeactivatedEvent;
	void Setup(List<PlayerAbilityModifier> modifiers);
	void Show();
    void UpdateButtonStatus();
	void Hide();
}

public interface PlayerAbilityModifierButtonsMediated
{
	event System.Action<PlayerAbilityModifier> buttonAdded;
	event System.Action buttonsShown;
	event System.Action buttonsHid;
	event System.Action allButtonsRemoved;
    event System.Action updateButtonStatus;
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
    public event System.Action updateButtonStatus;

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

    public void UpdateButtonStatus()
    {
        updateButtonStatus();
    }
}

