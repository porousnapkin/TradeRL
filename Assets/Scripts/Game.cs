using UnityEngine;

public class Game : MonoBehaviour {
	public PlayerHealthDisplay playerHealthDisplay;
	Character playerCharacter;

	void Start() {
		playerCharacter = new Character();

		playerHealthDisplay.health = playerCharacter.health;
	}	
}