using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class PlayerAbilityButtonsView : DesertView  {
	public GameObject buttonPrefab;
	public event System.Action<PlayerAbility> called;
	ButtonArranger buttonArranger;
	List<AbilityButton> buttons = new List<AbilityButton>();

	void Awake() 
	{
		buttonArranger = new ButtonArranger();
		buttonArranger.buttonPrefab = buttonPrefab;
		buttonArranger.parentTransform = transform;
	}

	public void AddButton(PlayerAbility ability) 
	{
		buttons.Add(buttonArranger.CreateButton(ability, ButtonHit));
		buttonArranger.ArrangeButtons(buttons);
	}

	void ButtonHit(PlayerActivatedPower ability)
	{
		//This feels hacky. Better way to do this?
		called(ability as PlayerAbility);
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
