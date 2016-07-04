using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.injector.api;

public class DesertContext : MVCSContext
{
	static ICrossContextInjectionBinder binder; 
	public static T StrangeNew<T>() where T : class
	{
		return binder.GetInstance<T>();
	}

	public static void QuickBind<T>(T value) where T : class
	{
		binder.Unbind<T>();
		binder.Bind<T>().ToValue(value);
		binder.GetBinding<T>().ToInject(false);
	}

	public static void FinishQuickBind<T>() where T : class {
		binder.Unbind<T>();
		BindClass<T>();
	}

	static void BindClass<T>() {
		binder.Bind<T>().To<T>();
	}

	public DesertContext (MonoBehaviour view) : base(view)
	{
	}
	
	public DesertContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
	{
	}
	
	protected override void mapBindings()
	{
		binder = injectionBinder;

		//Singleton bindings.
		injectionBinder.Bind<Effort>().ToSingleton ();
		injectionBinder.Bind<Inventory>().ToSingleton();
		injectionBinder.Bind<GameDate>().ToSingleton();
		injectionBinder.Bind<TownsAndCities>().ToSingleton();
		injectionBinder.Bind<MapData>().ToSingleton();
		injectionBinder.Bind<MapGraph>().ToSingleton();
		injectionBinder.Bind<LocationFactory>().ToSingleton();
		injectionBinder.Bind<ExpeditionFactory>().ToSingleton();
		injectionBinder.Bind<MapPlayerController>().ToSingleton();
		injectionBinder.Bind<PlayerSkills>().ToSingleton();
		injectionBinder.Bind<CombatGraph>().ToSingleton();
		injectionBinder.Bind<StoryFactory>().ToSingleton();
		injectionBinder.Bind<AICharacterFactory>().ToSingleton();
		injectionBinder.Bind<PlayerCombatCharacterFactory>().ToSingleton();
		injectionBinder.Bind<CityActionFactory>().ToSingleton();
		injectionBinder.Bind<MapCreator>().ToSingleton();
		injectionBinder.Bind<HiddenGrid>().ToSingleton();
		injectionBinder.Bind<GridInputCollector>().ToSingleton();
		injectionBinder.Bind<DooberFactory>().ToSingleton();
		injectionBinder.Bind<CombatFactory>().ToSingleton();
		injectionBinder.Bind<GlobalTextArea>().ToSingleton();
		injectionBinder.Bind<CombatModule>().ToSingleton();
		injectionBinder.Bind<TravelingStorySpawner>().ToSingleton();
        injectionBinder.Bind<FactionManager>().ToSingleton();

		//Switch this bind when we want actual combat in. Maybe should be switchable from Unity bool?
		injectionBinder.Bind<EncounterFactory>().To<StubEncounterFactory>().ToSingleton();
		//injectionBinder.Bind<EncounterFactory>().To<CombatEncounterFactory>().ToSingleton();

		//Construction binders
		BindClass<Location>();
		BindClass<ActivateAIAbility>();
		BindClass<AttackAbility>();
        BindClass<CustomAttackAbility>();
        BindClass<MoveInCombatAbility>();
        BindClass<UpdatePositionAnimation>();
		BindClass<AIAbility>();
		BindClass<AttackWithDamageMultiplierAbility>();
		BindClass<SingleTargetInputPicker>();
		BindClass<CombatDamageDooberHelper>();
        BindClass<AIAbilityTargetPicker>();
		BindClass<Expedition>();
		BindClass<GainCoinsEvent>();
		BindClass<MovePlayerToPreviousPositionEvent>();
		BindClass<StartCombatEvent>();
		BindClass<SkillStoryAction>();
		BindClass<Building>();
		BindClass<TravelingStory>();
		BindClass<Town>();
		BindClass<PlayerAbilityData>();
		BindClass<PlayerAbility>();
		BindClass<Character>();
		BindClass<TravelingStoryWander>();
		BindClass<TravelingStoryChase>();
		BindClass<TravelingStoryFlee>();
		BindClass<TravelingStoryBeginCombatAction>();
		BindClass<TravelingStoryBeginStoryAction>();
		BindClass<TravelingStoryAI>();
        BindClass<CombatController>();
        BindClass<Combat>();
        BindClass<Health>();
        BindClass<TargetHighlighter>();
        BindClass<TargetInputReciever>();

		//Named Singleton bindings.
		injectionBinder.Bind<Character>().ToSingleton().ToName(Character.PLAYER);
		injectionBinder.Bind<DesertPathfinder>().ToSingleton().ToName(DesertPathfinder.MAP);
		injectionBinder.Bind<DesertPathfinder>().ToSingleton().ToName(DesertPathfinder.COMBAT);

		//View / mediator bindings.
		mediationBinder.Bind<EffortDisplay>().To<EffortDisplayMediator>();
		mediationBinder.Bind<DaysDisplay>().To<DaysDisplayMediator>();
		mediationBinder.Bind<InventoryDisplay>().To<InventoryDisplayMediator>();
		mediationBinder.Bind<MapCreatorView>().To<MapCreatorMediator>();
		mediationBinder.Bind<HiddenGridView>().To<HiddenGridMediator>();
		mediationBinder.Bind<MapPlayerView>().To<MapPlayerViewMediator>();
		mediationBinder.Bind<GridInputCollectorView>().To<GridInputCollectorMediator>();
		mediationBinder.Bind<DooberFactoryView>().To<DooberFactoryMediator>();
		mediationBinder.Bind<CityDisplay>().To<CityDisplayMediator>();
		mediationBinder.Bind<TownDialog>().To<TownDialogMediator>();
		mediationBinder.Bind<AutoTravelButton>().To<AutoTravelButtonMediator>();
		mediationBinder.Bind<AbilityButton>().To<AbilityButtonMediator>();
		mediationBinder.Bind<MarketDisplay>().To<MarketDisplayMediator>();
		mediationBinder.Bind<ExpeditionScreen>().To<ExpeditionScreenMediator>();
		mediationBinder.Bind<PubScreen>().To<PubScreenMediator>();
		mediationBinder.Bind<BuildingScene>().To<BuildingSceneMediator>();
		mediationBinder.Bind<GlobalTextAreaView>().To<GlobalTextAreaMediator>();
		mediationBinder.Bind<TravelingStoryVisuals>().To<TravelingStoryVisualsMediator>();
		mediationBinder.Bind<HealthDisplay>().To<HealthDisplayMediator>();
		mediationBinder.Bind<PlayerHealthDisplay>().To<PlayerHealthDisplayMediator>();
		mediationBinder.Bind<ExpeditionPurchaseScreen>().To<ExpeditionPurchaseScreenMediator>();

		//Event/Command bindings
		commandBinder.Bind(ContextEvent.START).To<BeginGameCommand>().Once();

		//Injection binding.
		//Map a mock model and a mock service, both as Singletons

		//injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();


		//View/Mediator binding
		//This Binding instantiates a new ExampleMediator whenever as ExampleView
		//Fires its Awake method. The Mediator communicates to/from the View
		//and to/from the App. This keeps dependencies between the view and the app
		//separated.

		//mediationBinder.Bind<ExampleView>().To<ExampleMediator>();


		//Event/Command binding
		//commandBinder.Bind(ExampleEvent.REQUEST_WEB_SERVICE).To<CallWebServiceCommand>();

		//The START event is fired as soon as mappings are complete.
		//Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.
		//commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once ();
		
	}
}
