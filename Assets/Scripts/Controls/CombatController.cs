using UnityEngine;

public interface CombatController {
	void BeginTurn(System.Action turnFinished);
	Character GetCharacter();
    void SetWorldPosition(Vector3 position);
    void Attack(Character target, System.Action attackFinished);
}