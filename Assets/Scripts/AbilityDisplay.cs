using strange.extensions.mediation.impl;

public class AbilityDisplay : DesertView
{
    public TMPro.TextMeshProUGUI text;
    public UIImageRaycasterPopup popup;
    int popupSpace;

    protected override void Start()
    {
        base.Start();

        ClearAbility();
        popupSpace = popup.ReserveSpace();
    }

    public void SetAbility(string name, string description)
    {
        gameObject.SetActive(true);
        text.text = name;
        popup.Record(description, popupSpace);
    }

    public void ClearAbility()
    {
        gameObject.SetActive(false);
        text.text = "";
        popup.Record("", popupSpace);
    }
}

public class AbilityDisplayMediator : Mediator
{
    [Inject] public AbilityDisplay view { private get; set; }
    [Inject] public Character character { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        character.broadcastPreparedAIAbility += RespondToAbility;
        character.actionFinishedEvent += view.ClearAbility;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        character.broadcastPreparedAIAbility -= RespondToAbility;
        character.actionFinishedEvent -= view.ClearAbility;
    }

    private void RespondToAbility(AIAbility ability)
    {
        view.SetAbility(ability.abilityName, ability.abilityDescription);
    }
}

