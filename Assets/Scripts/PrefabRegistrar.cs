using UnityEngine;

public class PrefabRegistrar : MonoBehaviour {
	public GameObject abilityButtonPrefab;	
	public GameObject mainCanvas;
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;

	void Awake() {
		PlayerAbilityButtonFactory.buttonPrefab = abilityButtonPrefab;
		PlayerAbilityButtonFactory.abilityButtonCanvas = mainCanvas;

		AICharacterFactory.healthDisplayPrefab = healthDisplayPrefab;

		AICharacterFactory.dooberFactory = dooberFactory;
	}

}