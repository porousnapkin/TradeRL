using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmbushButtonsView : DesertView
{
    public GameObject abilityButtonPrefab;
    public GameObject confirmButtonPrefab;
    public event System.Action<PlayerAbility> called = delegate { };
    ButtonArranger buttonArranger;
    List<AbilityButton> buttons = new List<AbilityButton>();
    PlayerAbility selectedAbility;

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

    void ConfirmMove()
    {
        called(selectedAbility);

        selectedAbility = null;

        HideButtons();
        RemoveAllButtons();
    }

    void HideButtons()
    {
        buttons.ForEach(b => b.gameObject.SetActive(false));
    }

    void RemoveAllButtons() {
        buttons.Clear();
        buttonArranger.ArrangeButtons(buttons);
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
        button.SetUnselected ();
        button.Refund();
        selectedAbility = null;
        buttons.ForEach (b =>  {
            b.UpdateButtonStatus ();
        });
    }

    void SelectAbilityButton (PlayerAbility ability, AbilityButton button)
    {
        button.SetSelected ();
        selectedAbility = ability;
        buttons.ForEach (b =>  {
            if (b != button)
                b.button.interactable = false;
        });
        ConfirmMove();
    }

    public void ShowButtons()
    {
        buttons.ForEach(b => {
            b.gameObject.SetActive(true);
            b.SetUnselected();
            b.UpdateButtonStatus();
        });
    }
}

public class PlayerAmbushButtonsMediator : Mediator
{
    [Inject]
    public PlayerAmbushButtonsView view { private get; set; }
    [Inject]
    public PlayerAmbushButtonsMediated model { private get; set; }

    public override void OnRegister()
    {
        model.buttonAdded += view.AddButton;
        model.buttonsShown += view.ShowButtons;
        view.called += model.AmbushButtonHit;
    }

    public override void OnRemove()
    {
        model.buttonAdded -= view.AddButton;
        model.buttonsShown -= view.ShowButtons;
        view.called -= model.AmbushButtonHit;
    }
}

public interface PlayerAmbushButtons
{
    void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> callback);
}

public interface PlayerAmbushButtonsMediated
{
    event System.Action<PlayerAbility> buttonAdded;
    event System.Action buttonsShown;
    void AmbushButtonHit(PlayerAbility ability);
}

public class PlayerAmbushButtonsImpl : PlayerAmbushButtons, PlayerAmbushButtonsMediated
{
    public event System.Action<PlayerAbility> buttonAdded = delegate { };
    public event System.Action buttonsShown = delegate { };
    System.Action<PlayerAbility> abilityPickedCallback;

    public void Setup(List<PlayerAbility> abilities, System.Action<PlayerAbility> callback)
    {
        abilityPickedCallback = callback;
        abilities.ForEach(a => buttonAdded(a));
        buttonsShown();
    }

    public void AmbushButtonHit(PlayerAbility ability)
    {
        abilityPickedCallback(ability);
    }
}