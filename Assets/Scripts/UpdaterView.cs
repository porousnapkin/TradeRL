using strange.extensions.mediation.impl;

public class UpdaterView : DesertView
{
    public event System.Action UpdateEvent = delegate { };

    void Update()
    {
        UpdateEvent();
    }
}

public class UpdaterMediator : Mediator
{
    [Inject]public UpdaterView view { private get; set; }
    [Inject]public Updater model { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        view.UpdateEvent += model.CallUpdate;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        view.UpdateEvent -= model.CallUpdate;
    }
}

public class Updater
{
    public event System.Action OnUpdate = delegate { };

    public void CallUpdate()
    {
        OnUpdate();
    }
}
