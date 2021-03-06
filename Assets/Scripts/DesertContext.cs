﻿using UnityEngine;
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
		injectionBinder.Bind<Towns>().ToSingleton();
		injectionBinder.Bind<MapData>().ToSingleton();
		injectionBinder.Bind<MapGraph>().ToSingleton();
		injectionBinder.Bind<LocationFactory>().ToSingleton();
		injectionBinder.Bind<ExpeditionFactory>().ToSingleton();
		injectionBinder.Bind<MapPlayerController>().ToSingleton();
		injectionBinder.Bind<PlayerSkills>().ToSingleton();
		injectionBinder.Bind<PlayerCharacter>().ToSingleton();
		injectionBinder.Bind<StoryFactory>().ToSingleton();
		injectionBinder.Bind<AICharacterFactory>().ToSingleton();
		injectionBinder.Bind<PlayerCombatCharacterFactory>().ToSingleton();
		injectionBinder.Bind<CityActionFactory>().ToSingleton();
		injectionBinder.Bind<MapCreator>().ToSingleton();
		injectionBinder.Bind<HiddenGrid>().ToSingleton();
		injectionBinder.Bind<GridInputCollector>().ToSingleton();
		injectionBinder.Bind<DooberFactory>().ToSingleton();
		injectionBinder.Bind<GlobalTextArea>().ToSingleton();
		injectionBinder.Bind<CombatModule>().ToSingleton();
		injectionBinder.Bind<TravelingStorySpawner>().ToSingleton();
        injectionBinder.Bind<FactionManager>().ToSingleton();
        injectionBinder.Bind<PlayerTeam>().ToSingleton();
        injectionBinder.Bind<PlayerAbilityButtons>().Bind<PlayerAbilityButtonsMediated>().To<PlayerAbilityButtonsImpl>().ToSingleton();
		injectionBinder.Bind<PlayerAbilityModifierButtons>().Bind<PlayerAbilityModifierButtonsMediated>().To<AbilityModifierButtonsImpl>().ToSingleton();
        injectionBinder.Bind<CombatTurnOrderMediated>().Bind<CombatTurnOrderVisualizer>().To<CombatTurnOrderVisualizerImpl>().ToSingleton();
        injectionBinder.Bind<MapAbilityButtonsMediated>().Bind<MapAbilityButtons>().To<MapAbilityButtonsImpl>().ToSingleton();
	    injectionBinder.Bind<PlayerAmbushButtonsMediated>().Bind<PlayerAmbushButtons>().To<PlayerAmbushButtonsImpl>().ToSingleton();
        injectionBinder.Bind<ActiveLabelRequirements>().ToSingleton();
        injectionBinder.Bind<RandomEncounterGenerator>().ToSingleton();
        injectionBinder.Bind<TravelingStoryFactory>().ToSingleton();
        injectionBinder.Bind<TownEventLog>().ToSingleton();
        injectionBinder.Bind<Updater>().ToSingleton();
        injectionBinder.Bind<KeyboardInput>().ToSingleton();
        injectionBinder.Bind<GridMouseOverPopup>().ToSingleton();
        injectionBinder.Bind<MapViewData>().ToSingleton();
        injectionBinder.Bind<LocationMapData>().ToSingleton();

        //Construction binders
        BindClass<Location>();
		BindClass<ActivateAIAbility>();
		BindClass<AttackAbility>();
        BindClass<CustomAttackAbility>();
        BindClass<MoveInCombatAbility>();
        BindClass<UpdatePositionAnimation>();
		BindClass<AIAbility>();
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
		BindClass<TravelingStoryController>();
		BindClass<TravelingStoryMediated>();
		BindClass<Town>();
        BindClass<TownEconomy>();
        BindClass<TownPlayerBuildings>();
        BindClass<TownPlayerActions>();
        BindClass<Reputation>();
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
        BindClass<TargetInputReciever>();
        BindClass<PlayerCombatActor>();
        BindClass<TargetHighlighter>();
        BindClass<DistanceRestriction>();
		BindClass<ActivePlayerAbilityModifiers>();
		BindClass<AbilityDamageModifier>();
		BindClass<AbilityInitiativeModifier>();
		BindClass<PlayerAbilityModifier>();
		BindClass<PlayerSkill>();
        BindClass<CombatFactory>();
        BindClass<EffortCost>();
        BindClass<MapPlayerAbility>();
        BindClass<MapAbilityStartStoryActivator>();
        BindClass<MapLocationNotOnEventRestriction>();
        BindClass<CreateMapLocationEvent>();
        BindClass<StartStoryActionEvent>();
        BindClass<GainEffortEvent>();
        BindClass<GainHealthEvent>();
        BindClass<GainHealthAfterCombat>();
        BindClass<CreateMapAbilityButtonItemEffect>();
        BindClass<Item>();
        BindClass<ItemCost>();
        BindClass<ChangeItemsEvent>();
        BindClass<StatusEffect>();
        BindClass<DaysEffectDuration>();
        BindClass<RestEvent>();
        BindClass<CanRestRestriction>();
        BindClass<MakeRestImpossibleEffectAction>();
        BindClass<AdvanceDaysEvent>();
        BindClass<ChangeInitiativeAbility>();
        BindClass<CreateCombatAbilityButtonItemEffect>();
        BindClass<MultiActivatorAbility>();
        BindClass<JamItemAbilityActivator>();
        BindClass<ItemIsUnjammedRestriction>();
        BindClass<CharacterCreationDataHelper>();
        BindClass<LabelRequiredAttackBonus>();
        BindClass<DodgeDefenseMod>();
		BindClass<EffortPoolIncreaser>();
		BindClass<JamSavesIncreaser>();
		BindClass<MapAbilityGunUnjammerActivator>();
		BindClass<HasJammedGunRestriction>();
		BindClass<MapAbilityGainer>();
        BindClass<MultiAnimation>();
        BindClass<AdditionalAbilityActivatorModifier>();
        BindClass<MultiTargetPicker>();
        BindClass<AllyStabilizationEvent>();
        BindClass<ShieldDefenseMod>();
        BindClass<ShieldGeneratorEquipment>();
        BindClass<ShieldGeneratorEffect>();
        BindClass<MapAbilitySummonAllyActivator>();
        BindClass<HasXInitiativeRestriction>();
        BindClass<AbilityAndAbilityModifierAbility>();
        BindClass<CounterAttack>();
        BindClass<TravelingStoryGuard>();
        BindClass<TownGainsActionBenefit>();
        BindClass<TownUpgradeTracks>();
        BindClass<StatusEffects>();
        BindClass<RestModule>();
        BindClass<GainGoldWhenRestingTownStatus>();
        BindClass<TownGainsStatusEffectBenefit>();
        BindClass<TownGainsPoliticalReputationBenefit>();
        BindClass<GainPoliticalInfluenceWhenRestingTownStatus>();
        BindClass<TownTradeBonus>();
        BindClass<GainTradeBonusTownStatus>();
        BindClass<ShieldGeneratorAbility>();
        BindClass<GainCitizenReputationAtTownStoryEvent>();

        //Named Singleton bindings.
        injectionBinder.Bind<DesertPathfinder>().ToSingleton().ToName(DesertPathfinder.MAP);
        injectionBinder.Bind<StatusEffects>().ToSingleton().ToName(StatusEffects.PARTY);

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
		mediationBinder.Bind<MarketDisplay>().To<MarketDisplayMediator>();
		mediationBinder.Bind<ExpeditionScreen>().To<ExpeditionScreenMediator>();
		mediationBinder.Bind<PubScreen>().To<PubScreenMediator>();
		mediationBinder.Bind<BuildingScene>().To<BuildingSceneMediator>();
		mediationBinder.Bind<GlobalTextAreaView>().To<GlobalTextAreaMediator>();
		mediationBinder.Bind<TravelingStoryVisuals>().To<TravelingStoryVisualsMediator>();
        mediationBinder.Bind<PlayerAbilityButtonsView>().To<PlayerAbilityButtonsMediator>();
		mediationBinder.Bind<PlayerAbilityModifierButtonsView>().To<PlayerAbilityModifierButtonsMediator>();
		mediationBinder.Bind<HealthDisplay>().To<HealthDisplayMediator>();
		mediationBinder.Bind<PlayerHealthDisplay>().To<PlayerHealthDisplayMediator>();
		mediationBinder.Bind<ExpeditionPurchaseScreen>().To<ExpeditionPurchaseScreenMediator>();
		mediationBinder.Bind<CombatTurnOrderView>().To<CombatTurnOrderMediator>();
        mediationBinder.Bind<DebugCharacterCreator>().To<DebugCharacterCreatorMediator>();
        mediationBinder.Bind<DebugTeamCreator>().To<DebugTeamCreatorMediator>();
	    mediationBinder.Bind<MapAbilityButtonsView>().To<MapAbilityButtonsMediator>();
	    mediationBinder.Bind<PlayerAmbushButtonsView>().To<PlayerAmbushButtonsMediator>();
	    mediationBinder.Bind<PartyStatusVisuals>().To<PartyStatusMediator>();
        mediationBinder.Bind<CityRestButton>().To<CityRestButtonMediator>();
        mediationBinder.Bind<TravelSuppliesDisplay>().To<TravelSuppliesDisplayMediator>();
        mediationBinder.Bind<TeammateInfoList>().To<TeammateInfoListMediator>();
        mediationBinder.Bind<RepairAllItemsButton>().To<RepairAllItemsButtonMediator>();
        mediationBinder.Bind<HireTeammatePanel>().To<HireTeammatePanelMediator>();
        mediationBinder.Bind<HireAlliesScreen>().To<HireAlliesScreenMediator>();
        mediationBinder.Bind<ShieldDisplay>().To<ShieldDisplayMediator>();
        mediationBinder.Bind<CityDetailsDisplay>().To<CityDetailsDisplayMediator>();
        mediationBinder.Bind<TownEventLogDisplay>().To<TownEventLogDisplayMediator>();
        mediationBinder.Bind<UpdaterView>().To<UpdaterMediator>();
        mediationBinder.Bind<CreateQuestButton>().To<CreateQuestButtonMediator>();
        mediationBinder.Bind<DamageDisplay>().To<DamageDisplayMediator>();
        mediationBinder.Bind<AbilityDisplay>().To<AbilityDisplayMediator>();

		//Event/Command bindings
		commandBinder.Bind(ContextEvent.START).To<BeginGameCommand>().Once();
	}
}
