using strange.extensions.mediation.impl;

public class DamageDisplay : DesertView
{
    public TMPro.TextMeshProUGUI text;

    public void SetDamage(int damage)
    {
        gameObject.SetActive(damage > 0);
        text.text = damage.ToString();
    }
}

public class DamageDisplayMediator : Mediator
{
    [Inject]
    public DamageDisplay view { private get; set; }
    [Inject]
    public Character character { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        view.SetDamage(0);
        character.broadcastPreparedAttackEvent += RespondToBroadcastAttack;
    }

    private void RespondToBroadcastAttack(AttackData attack)
    {
        view.SetDamage(attack.totalDamage);
    }

    public override void OnRemove()
    {
        base.OnRemove();

        character.broadcastPreparedAttackEvent -= RespondToBroadcastAttack;
    }
}
