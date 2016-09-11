using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICombatVisuals {
	void Setup(int combatSize, Vector2 combatCenter);
	void PlayFinished(System.Action returnPlayerToStartingPosition);
}

public class CombatVisuals : MonoBehaviour, ICombatVisuals {
	public GameObject combatEdgePrefab;
	public Animator animator;
	public List<SpriteRenderer> enemySprites;
	List<SpriteRenderer> extentSprites = new List<SpriteRenderer>();

	public void Setup(int combatSize, Vector2 combatCenter) {
		for(int x = -combatSize; x <= combatSize; x++) {
			for(int y = -combatSize; y <= combatSize; y++) {
				if(!(y == combatSize || y == -combatSize || x == combatSize || x == -combatSize))
					continue;

				SetupEdge(combatCenter + new Vector2(x, y));
			}
		}

		StartCoroutine(SetupCoroutine());
	}	

	int edgeNum = 0;
	void SetupEdge(Vector2 position) {
		var go = GameObject.Instantiate(combatEdgePrefab) as GameObject;
		go.name += edgeNum;
		edgeNum++;
		go.transform.parent = transform;
		go.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
		var s = go.GetComponent<SpriteRenderer>();
		extentSprites.Add(s);
		s.color = new Color(s.color.r, s.color.g, s.color.b, 0);
	}

	IEnumerator SetupCoroutine() {
		animator.Play("BeginCombat");

		LeanTween.value(enemySprites[0].gameObject, FadeEnemies, 0.0f, 1.0f, 0.5f);

		yield return new WaitForSeconds(0.5f);

		LeanTween.value(extentSprites[0].gameObject, FadeExtents, 0.0f, 1.0f, 0.5f);
	}

	void FadeEnemies(float alpha) {
		foreach(var s in enemySprites)
			s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);
	}

	void FadeExtents(float alpha) {
		foreach(var s in extentSprites)
			s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);
	}

	public void PlayFinished(System.Action returnPlayerToStartingPosition) {
		StartCoroutine(FinishCombatCoroutine(returnPlayerToStartingPosition));
	}

	IEnumerator FinishCombatCoroutine(System.Action returnPlayerToStartingPosition) {
		animator.Play("EndCombat");

		yield return new WaitForSeconds(0.5f);

		returnPlayerToStartingPosition();

		yield return new WaitForSeconds(0.5f);

		LeanTween.value(extentSprites[0].gameObject, FadeExtents, 1.0f, 0.0f, 0.5f);

		yield return new WaitForSeconds(0.5f);

		CleanUp();
	}

	void CleanUp() {
		GameObject.Destroy(gameObject);
	}
}

