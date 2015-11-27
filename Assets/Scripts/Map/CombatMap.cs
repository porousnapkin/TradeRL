using UnityEngine;
using System.Collections;

public class CombatMap {
	public int width = 10;
	public int height = 10;
	public Sprite sprite = null;
	public GridInputCollector inputCollector = null;
	public Transform combatParent;

	public void Setup() {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				CreateSprite(x, y);
			}
		}
	}

	void CreateSprite(int x, int y) {
		var worldPos = Grid.GetBaseCombatPosition(x, y);
		var spriteRenderer = MapCreator.CreateSpriteAtPosition(sprite, worldPos, x, y, "Combat", inputCollector);
		spriteRenderer.transform.SetParent(combatParent);
	}
}
