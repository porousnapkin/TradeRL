using UnityEngine;

public class CombatDamageDooberHelper {
	DooberFactory dooberFactory;
	public CombatDamageDooberHelper(Health health, CombatModule combatModule, Character character, DooberFactory df) {
		dooberFactory = df;
		health.DamagedEvent += (val) => CreateDamageDoober(GetCharacterWorldPosition(character), val);
		combatModule.MissedEvent += CreateMissDoober;
	}

	Vector3 GetCharacterWorldPosition(Character c) {
		return Grid.GetCharacterWorldPositionFromGridPositon((int)c.WorldPosition.x, (int)c.WorldPosition.y);
	}

	void CreateDamageDoober(Vector3 baseLocation, int amount) {
		dooberFactory.CreateDamageDoober(baseLocation, amount);
	}

	void CreateMissDoober(Character target) {
		dooberFactory.CreateMissDoober(GetCharacterWorldPosition(target));
	}
}