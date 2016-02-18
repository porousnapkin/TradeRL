using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public static class PrefabGetter {
	class ResourceGetter<T> where T : UnityEngine.Object {
		public string path;
		T prefab;

		public T Get() {
			if(prefab == null)
				prefab = Resources.Load<T>(path);

			return prefab;
		}
	}

	class TransformGetter {
		public string path;
		Transform t;

		public Transform Get() {
			if(t == null)
				t = GameObject.Find(path).transform;
			return t;
		}
	}

	static ContextView contextViewStore;
	public static ContextView contextView {
		get {
			if(contextViewStore == null)
				contextViewStore = applicationRoot.GetComponent<ContextView>();
			return contextViewStore;
		}
	}

	static TransformGetter applicationRootGetter = new TransformGetter { path = "ApplicationRoot" };
	public static Transform applicationRoot { get { return applicationRootGetter.Get (); }}

	static TransformGetter baseCanvasGetter = new TransformGetter { path = "ApplicationRoot/Canvas" };
	public static Transform baseCanvas { get { return baseCanvasGetter.Get(); } }

	static TransformGetter playerAbilityButtonsParentGetter = new TransformGetter { path = "ApplicationRoot/PlayerAbilityButtons" };
	public static Transform playerAbilityButtonsParent { get { return playerAbilityButtonsParentGetter.Get(); } }

	static ResourceGetter<GameObject> abilityButtonPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/UI/AbilityButton" };
	public static GameObject abilityButtonPrefab { get { return abilityButtonPrefabGetter.Get(); } }

	static ResourceGetter<GameObject> cityDisplayPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/CityAction/CityDialog" };
	public static GameObject cityDisplayPrefab { get { return cityDisplayPrefabGetter.Get(); } }

	static ResourceGetter<GameObject> travelingStoryPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/Art/TravelingStoryVis" };
	public static GameObject travelingStoryPrefab { get { return travelingStoryPrefabGetter.Get(); } }
		
	static ResourceGetter<GameObject> storyVisualsPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/Story/StoryEvent" };
	public static GameObject storyVisualsPrefab { get { return storyVisualsPrefabGetter.Get(); } }
	
	static ResourceGetter<GameObject> skillStoryActionPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/Story/SkillStoryAction" };
	public static GameObject skillStoryActionPrefab { get { return skillStoryActionPrefabGetter.Get(); } }
	
	static ResourceGetter<GameObject> storyActionPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/Story/StoryAction" };
	public static GameObject storyActionPrefab { get { return storyActionPrefabGetter.Get(); } }

	static ResourceGetter<GameObject> healthDisplayPrefabGetter = new ResourceGetter<GameObject> { path = "Prefabs/HealthDisplay" };
	public static GameObject healthDisplayPrefab { get { return healthDisplayPrefabGetter.Get(); } }
}
