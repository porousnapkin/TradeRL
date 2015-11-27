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
	public StoryData combatEdgeStoryData;
	public GameObject marketPrefab;
	public GameObject townCenterPrefab;
	public GameObject townDisplayPrefab;
	public GameObject expeditionPrefab;
	public GameObject pubPrefab;
	public GameObject buildingScenePrefab;
	public GameObject travelingStoryPrefab;
	public GameObject combatMapPrefab;
	public Sprite combatSprite;

	void Awake() {
		PlayerAbilityButtonFactory.buttonPrefab = abilityButtonPrefab;
		PlayerAbilityButtonFactory.abilityButtonCanvas = mainCanvas;
		PlayerAbilityButtonFactory.buttons = buttons;

		AICharacterFactory.healthDisplayPrefab = healthDisplayPrefab;
		AICharacterFactory.dooberFactory = dooberFactory;

		AIActionFactory.dooberFactory = dooberFactory;

		AbilityFactory.dooberFactory = dooberFactory;

		CombatFactory.combatVisualsPrefab = combatVisualsPrefab;
		CombatFactory.combatEdgeStoryData = combatEdgeStoryData;
		CombatFactory.combatMapSprite = combatSprite;
		CombatFactory.combatMapPrefab = combatMapPrefab;

		StoryFactory.storyVisualsPrefab = storyVisualsPrefab.gameObject;
		StoryFactory.storyCanvas = mainCanvas.transform;
		StoryFactory.skillStoryActionPrefab = skillStoryActionPrefab.gameObject;
		StoryFactory.storyActionPrefab = storyActionPrefab.gameObject;

		CityActionFactory.marketPrefab = marketPrefab;
		CityActionFactory.cityCenterPrefab = townCenterPrefab;
		CityActionFactory.cityDisplayPrefab = townDisplayPrefab;
		CityActionFactory.expeditionPrefab = expeditionPrefab;
		CityActionFactory.pubPrefab = pubPrefab;
		CityActionFactory.buildingScenePrefab = buildingScenePrefab;

		TravelingStoryFactory.travelingStoryPrefab = travelingStoryPrefab;
	}
}