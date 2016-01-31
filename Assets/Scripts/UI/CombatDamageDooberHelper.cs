using UnityEngine;

public class CombatDamageDooberHelper {
	[Inject] public DooberFactory dooberFactory { private get; set; }
	
	public void Setup(Health health, Character character) {
		health.DamagedEvent += (val) => CreateDamageDoober(GetCharacterWorldPosition(character), val);
		health.HealedEvent += (val) => CreateHealDoober(character, val);
	}

	Vector3 GetCharacterWorldPosition(Character c) {
		return Grid.GetCharacterWorldPositionFromGridPositon((int)c.Position.x, (int)c.Position.y);
	}

	void CreateDamageDoober(Vector3 baseLocation, int amount) {
		dooberFactory.CreateDamageDoober(baseLocation, amount);
	}

	void CreateMissDoober(Character target) {
		dooberFactory.CreateMissDoober(GetCharacterWorldPosition(target));
	}

	void CreateHealDoober(Character target, int amount) {
		dooberFactory.CreateHealDoober(GetCharacterWorldPosition(target), amount);
	}
}