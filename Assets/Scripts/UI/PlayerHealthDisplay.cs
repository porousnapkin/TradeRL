using strange.extensions.mediation.impl;

public class PlayerHealthDisplay : HealthDisplay {
}

public class PlayerHealthDisplayMediator : Mediator {
	[Inject] public PlayerHealthDisplay view { private get; set; }	
    [Inject (Character.PLAYER)] public Character playerCharacter { private get; set; }
    HealthDisplayMediator mediator;

	public override void OnRegister()
    {
        mediator = new HealthDisplayMediator();
        mediator.view = view;
        mediator.model = playerCharacter.health;

        mediator.OnRegister();
    }

	public override void OnRemove()
    {
        mediator.OnRemove();
    }
}