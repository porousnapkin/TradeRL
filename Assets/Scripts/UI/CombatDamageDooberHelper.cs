using UnityEngine;

public class CombatDamageDooberHelper {
	[Inject] public DooberFactory dooberFactory { private get; set; }
	
	public void Setup(Health health, GameObject art) {
		health.DamagedEvent += (val) => CreateDamageDoober(art.transform.position, val);
		health.HealedEvent += (val) => CreateHealDoober(art.transform.position, val);
	}

	void CreateDamageDoober(Vector3 baseLocation, int amount) {
		dooberFactory.CreateDamageDoober(baseLocation, amount);
	}

	void CreateMissDoober(Vector3 baseLocation) {
		dooberFactory.CreateMissDoober(baseLocation);
	}

	void CreateHealDoober(Vector3 baseLocation, int amount) {
		dooberFactory.CreateHealDoober(baseLocation, amount);
	}
}