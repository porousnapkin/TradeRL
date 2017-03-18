using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using System;

public class PlayerAbilityButtonsView : DesertView  {
	public GameObject abilityButtonPrefab;
	public GameObject confirmButtonPrefab;
	public event System.Action<PlayerAbility> called = delegate { };
    public event Action<PlayerAbility> uncalled = delegate { };
	ButtonArranger buttonArranger;
	List<AbilityButton> buttons = new List<AbilityButton>();
	PlayerAbility selectedAbility;
    bool currentlyActivatingAbility = false;

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

	public void AddButton(PlayerAbility ability) 
	{
		var button = buttonArranger.CreateButton(ability);
		button.called += (a) => ButtonHit(button, a);
		buttons.Add(button);
		buttonArranger.ArrangeButtons(buttons);
	}

	void ButtonHit(AbilityButton button, PlayerActivatedPower power)
	{		
		//This feels hacky. Better way to do this?
		var ability = power as PlayerAbility;

		if(ability == selectedAbility)
			UnselectAbilityButton (button);
		else
			SelectAbilityButton (ability, button);
	}

	void UnselectAbilityButton (AbilityButton button)
	{
        currentlyActivatingAbility = false;

		button.SetUnselected ();
        button.Refund();
        uncalled(selectedAbility);
		selectedAbility = null;
		buttons.ForEach (b =>  {
			b.UpdateButtonStatus ();
		});
    }

	void SelectAbilityButton (PlayerAbility ability, AbilityButton button)
	{
        currentlyActivatingAbility = true;

        button.SetSelected ();
		selectedAbility = ability;
		buttons.ForEach (b =>  {
			if (b != button)
				b.button.interactable = false;
		});
		called(selectedAbility);
	}

    public void ShowButtons()
    {
        if (currentlyActivatingAbility)
            return;

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
        buttons.ForEach(b => GameObject.Destroy(b.gameObject));
        buttons.Clear();
        buttonArranger.ArrangeButtons(buttons);
	}

    public void FinishedUsingAbility()
    {
        currentlyActivatingAbility = false;
        selectedAbility = null;
        ShowButtons();
    }
}

public class PlayerAbilityButtonsMediator : Mediator {
	[Inject] public PlayerAbilityButtonsView view { private get; set; }
	[Inject] public PlayerAbilityButtonsMediated model { private get; set; }

	public override void OnRegister ()
	{
        model.buttonAdded += view.AddButton;
        model.buttonsShown += view.ShowButtons;
        model.buttonsHid += view.HideButtons;
        model.allButtonsRemoved += view.RemoveAllButtons;
        model.finishedUsingAbility += view.FinishedUsingAbility;
        view.called += model.AbilityButtonHit;
        view.uncalled += model.AbilityButtonUnselected;
	}
		
	public override void OnRemove()
	{
		model.buttonAdded -= view.AddButton;
		model.buttonsShown -= view.ShowButtons;
		model.buttonsHid -= view.HideButtons;
		model.allButtonsRemoved -= view.RemoveAllButtons;
        model.finishedUsingAbility -= view.FinishedUsingAbility;
		view.called -= model.AbilityButtonHit;
        view.uncalled -= model.AbilityButtonUnselected;
	}
}

public interface PlayerAbilityButtons
{
    void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> selected, System.Action<PlayerAbility> unselected);
    void ShowButtons();
    void HideButtons();
    void FinishedUsingAbility();
}

public interface PlayerAbilityButtonsMediated
{
    event System.Action<PlayerAbility> buttonAdded;
    event System.Action buttonsShown;
    event System.Action buttonsHid;
    event System.Action allButtonsRemoved;
    event System.Action finishedUsingAbility;
    void AbilityButtonHit(PlayerAbility ability);
    void AbilityButtonUnselected(PlayerAbility ability);
}

public class PlayerAbilityButtonsImpl : PlayerAbilityButtons, PlayerAbilityButtonsMediated
{
    public event System.Action<PlayerAbility> buttonAdded = delegate { };
    public event System.Action buttonsShown = delegate { };
    public event System.Action buttonsHid = delegate { };
    public event System.Action allButtonsRemoved = delegate { };
    public event System.Action finishedUsingAbility = delegate { };
    System.Action<PlayerAbility> abilityPickedCallback;
    System.Action<PlayerAbility> abilityUnpickedCallback;

    public void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> selected, System.Action<PlayerAbility> unselected)
    {
        abilityPickedCallback = selected;
        abilityUnpickedCallback = unselected;
        allButtonsRemoved();
        abilities.ForEach(a => buttonAdded(a));
    }

    public void ShowButtons()
    {
        buttonsShown();
    }

    public void HideButtons()
    {
        buttonsHid();
    }

    public void FinishedUsingAbility()
    {
        finishedUsingAbility();
    }

    public void AbilityButtonHit(PlayerAbility ability)
    {
        abilityPickedCallback(ability);
    }

    public void AbilityButtonUnselected(PlayerAbility ability)
    {
        abilityUnpickedCallback(ability);
    }
}
