using strange.extensions.mediation.impl;

public class PlayerHealthDisplay : HealthDisplay {
}

public class PlayerHealthDisplayMediator : Mediator {
	[Inject] public PlayerHealthDisplay view { private get; set; }	
	[Inject] public PlayerCharacter party { private get; set; }
    HealthDisplayMediator mediator;

	public override void OnRegister()
    {
        mediator = gameObject.AddComponent<HealthDisplayMediator>();
        mediator.view = view;
		mediator.model = party.GetCharacter().health;

        mediator.OnRegister();
    }

	public override void OnRemove()
    {
        mediator.OnRemove();
    }
}