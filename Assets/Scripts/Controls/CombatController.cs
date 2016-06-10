using UnityEngine;

public interface CombatController {
    event System.Action ActEvent;

    void RollInitiative();
    int GetInitiative();
	void BeginTurn(System.Action turnFinished);
	Character GetCharacter();
    void SetWorldPosition(Vector3 position);
    void Attack(Character target, System.Action attackFinished);
}