using UnityEngine;

public class CombatDamageDooberHelper {
	[Inject] public DooberFactory dooberFactory { private get; set; }
    Transform transform;
    Health health;
	
	public void Setup(Health health, GameObject art) {
        this.health = health;
        this.transform = art.transform;

		health.DamagedEvent += CreateDamageDoober;
		health.HealedEvent += CreateHealDoober;
        health.KilledEvent += Cleanup;
        GlobalEvents.CombatEnded += Cleanup;
	}

    void Cleanup()
    {
        GlobalEvents.CombatEnded -= Cleanup;
        health.KilledEvent -= Cleanup;
		health.DamagedEvent -= CreateDamageDoober;
		health.HealedEvent -= CreateHealDoober;
    }

	void CreateDamageDoober(int amount) {
		dooberFactory.CreateDamageDoober(transform.position, amount);
	}

	void CreateHealDoober(int amount) {
		dooberFactory.CreateHealDoober(transform.position, amount);
	}
}