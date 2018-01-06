using strange.extensions.mediation.impl;

//TODO: Do we need to do stuff in here?
public class ExpeditionScreen : CityActionDisplay{
    public event System.Action beginExpeditionEvent;
    bool justCreated = true;

    void OnEnable()
    {
        if (justCreated)
            justCreated = false;
        else
            beginExpeditionEvent();
    }
}

public class ExpeditionScreenMediator : Mediator {
	[Inject] public ExpeditionScreen view {private get; set;}
	[Inject] public Town town {private get; set; }
	[Inject] public Inventory Inventory {private get; set; }
	[Inject] public ExpeditionFactory expeditionFactory { private get; set; }
	[Inject] public CityActionFactory cityActionFactory {private get; set; }

	public override void OnRegister ()
	{
        view.beginExpeditionEvent += BeginExpedition;
	}

    public override void OnRemove()
    {
        base.OnRemove();
        view.beginExpeditionEvent -= BeginExpedition;
    }

    void BeginExpedition()
    {
        expeditionFactory.BeginExpedition(town);
		cityActionFactory.DestroyCity();
    }
}
