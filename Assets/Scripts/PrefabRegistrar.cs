using UnityEngine;

public class PrefabRegistrar : MonoBehaviour {
	public GameObject abilityButtonPrefab;	
	public GameObject mainCanvas;
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;
	public PlayerAbilityButtons buttons;

	void Awake() {
		PlayerAbilityButtonFactory.buttonPrefab = abilityButtonPrefab;
		PlayerAbilityButtonFactory.abilityButtonCanvas = mainCanvas;
		PlayerAbilityButtonFactory.buttons = buttons;

		AICharacterFactory.healthDisplayPrefab = healthDisplayPrefab;

		AICharacterFactory.dooberFactory = dooberFactory;
	}

}