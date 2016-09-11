using UnityEngine;

public class CombatDamageDooberHelper {
	[Inject] public DooberFactory dooberFactory { private get; set; }
    Transform position;
    Health health;
	
	public void Setup(Health health, GameObject art) {
        this.health = health;
        this.position = art.transform;

		health.DamagedEvent += CreateDamageDoober;
		health.HealedEvent += CreateHealDoober;
        GlobalEvents.CombatEnded += Cleanup;
	}

    void Cleanup()
    {
        GlobalEvents.CombatEnded -= Cleanup;
		health.DamagedEvent -= CreateDamageDoober;
		health.HealedEvent -= CreateHealDoober;
    }

	void CreateDamageDoober(int amount) {
		dooberFactory.CreateDamageDoober(position.position, amount);
	}

	void CreateHealDoober(int amount) {
		dooberFactory.CreateHealDoober(position.position, amount);
	}
}