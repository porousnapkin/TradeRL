using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class PlayerAbilityButtonsView : DesertView  {
	public GameObject abilityButtonPrefab;
	public GameObject confirmButtonPrefab;
	public event System.Action<PlayerAbility> called = delegate { };
	ButtonArranger buttonArranger;
	List<AbilityButton> buttons = new List<AbilityButton>();
	GameObject confirmGO;
	PlayerAbility selectedAbility;

	protected override void Awake() 
	{
        base.Awake();

		SetupButtonArranger();
		SetupConfirmButton ();
	}

	void SetupButtonArranger()
	{
		buttonArranger = new ButtonArranger();
		buttonArranger.buttonPrefab = abilityButtonPrefab;
		buttonArranger.parentTransform = transform;
	}

	void SetupConfirmButton ()
	{
		confirmGO = GameObject.Instantiate (confirmButtonPrefab) as GameObject;
		confirmGO.transform.SetParent (transform, false);
		confirmGO.GetComponent<ButtonHelper>().pointerDownEvent += ConfirmMove;
	}

	void ConfirmMove()
	{
		called(selectedAbility);

		selectedAbility = null;
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
		confirmGO.SetActive(false);
		button.SetUnselected ();
        button.Refund();
		selectedAbility = null;
		buttons.ForEach (b =>  {
			b.UpdateButtonStatus ();
		});
	}

	void SelectAbilityButton (PlayerAbility ability, AbilityButton button)
	{
		confirmGO.SetActive(true);
		button.SetSelected ();
		selectedAbility = ability;
		buttons.ForEach (b =>  {
			if (b != button)
				b.button.interactable = false;
		});
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
		confirmGO.SetActive(false);
    }

	public void RemoveAllButtons() {
        buttons.ForEach(b => GameObject.Destroy(b.gameObject));
        buttons.Clear();
        buttonArranger.ArrangeButtons(buttons);
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
        view.called += model.AbilityButtonHit;
	}
		
	public override void OnRemove()
	{
		model.buttonAdded -= view.AddButton;
		model.buttonsShown -= view.ShowButtons;
		model.buttonsHid -= view.HideButtons;
		model.allButtonsRemoved -= view.RemoveAllButtons;
		view.called -= model.AbilityButtonHit;
	}
}

public interface PlayerAbilityButtons
{
    void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> callback);
    void ShowButtons();
    void HideButtons();
}

public interface PlayerAbilityButtonsMediated
{
    event System.Action<PlayerAbility> buttonAdded;
    event System.Action buttonsShown;
    event System.Action buttonsHid;
    event System.Action allButtonsRemoved;
    void AbilityButtonHit(PlayerAbility ability);
}

public class PlayerAbilityButtonsImpl : PlayerAbilityButtons, PlayerAbilityButtonsMediated
{
    public event System.Action<PlayerAbility> buttonAdded = delegate { };
    public event System.Action buttonsShown = delegate { };
    public event System.Action buttonsHid = delegate { };
    public event System.Action allButtonsRemoved = delegate { };
    System.Action<PlayerAbility> abilityPickedCallback;

    public void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> callback)
    {
        abilityPickedCallback = callback;
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

    public void AbilityButtonHit(PlayerAbility ability)
    {
        abilityPickedCallback(ability);
    }
}
