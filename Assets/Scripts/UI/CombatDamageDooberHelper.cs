using UnityEngine;

public class CombatDamageDooberHelper {
	DooberFactory dooberFactory;
	public CombatDamageDooberHelper(Health health, Character character, DooberFactory df) {
		dooberFactory = df;
		health.DamagedEvent += (val) => CreateDamageDoober(GetCharacterWorldPosition(character), val);
		health.HealedEvent += (val) => CreateHealDoober(character, val);
	}

	Vector3 GetCharacterWorldPosition(Character c) {
		return Grid.GetCharacterWorldPositionFromGridPositon((int)c.GraphPosition.x, (int)c.GraphPosition.y);
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