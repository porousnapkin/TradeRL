using UnityEngine;

public class HiddenGrid : MonoBehaviour {
	public MapCreator map;
	public int sightDistance = 5;
	public bool startHidden = true;

	void Start() {
		if(!startHidden)
			for(int x = 0; x < map.width; x++)
				for(int y = 0; y < map.height; y++)
					map.HideSprite(x, y);	
	}

	public void SetPosition(Vector2 position) {
		for(int x = (int)position.x - sightDistance; x < position.x + sightDistance; x++)
			for(int y = (int)position.y - sightDistance; y < position.y + sightDistance; y++)
				if(Vector2.Distance(position, new Vector2(x, y)) < sightDistance)
					map.ShowSprite(x, y);
	}
}