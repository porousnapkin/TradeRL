using UnityEngine;

public interface ICombatVisuals {
	void Setup(int combatSize, Vector2 combatCenter);
	void CleanUp();
}

public class CombatVisuals : MonoBehaviour, ICombatVisuals {
	public GameObject _combatEdgePrefab;

	public void Setup(int combatSize, Vector2 combatCenter) {
		for(int x = -combatSize; x <= combatSize; x++) {
			for(int y = -combatSize; y <= combatSize; y++) {
				if(!(y == combatSize || y == -combatSize || x == combatSize || x == -combatSize))
					continue;

				SetupEdge(combatCenter + new Vector2(x, y));
			}
		}
	}	

	void SetupEdge(Vector2 position) {
		var go = GameObject.Instantiate(_combatEdgePrefab) as GameObject;
		go.transform.parent = transform;
		go.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
	}

	public void CleanUp() {
		GameObject.Destroy(gameObject);
	}
}

