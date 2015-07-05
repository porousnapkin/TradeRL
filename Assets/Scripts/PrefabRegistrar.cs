using UnityEngine;

public class PrefabRegistrar : MonoBehaviour {
	public GameObject abilityButtonPrefab;	
	public GameObject mainCanvas;
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;
	public PlayerAbilityButtons buttons;
	public CombatVisuals combatVisualsPrefab;
	public StoryVisuals storyVisualsPrefab;
	public SkillStoryActionVisuals skillStoryActionPrefab;
	public StoryActionVisuals storyActionPrefab;

	void Awake() {
		PlayerAbilityButtonFactory.buttonPrefab = abilityButtonPrefab;
		PlayerAbilityButtonFactory.abilityButtonCanvas = mainCanvas;
		PlayerAbilityButtonFactory.buttons = buttons;

		AICharacterFactory.healthDisplayPrefab = healthDisplayPrefab;
		AICharacterFactory.dooberFactory = dooberFactory;

		AIActionFactory.dooberFactory = dooberFactory;

		AbilityFactory.dooberFactory = dooberFactory;

		CombatFactory.combatVisualsPrefab = combatVisualsPrefab;

		StoryFactory.storyVisualsPrefab = storyVisualsPrefab.gameObject;
		StoryFactory.storyCanvas = mainCanvas.transform;
		StoryFactory.skillStoryActionPrefab = skillStoryActionPrefab.gameObject;
		StoryFactory.storyActionPrefab = storyActionPrefab.gameObject;
	}

}