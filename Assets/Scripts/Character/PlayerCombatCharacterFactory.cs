using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerCombatCharacterFactory {
    [Inject]
    public FactionManager factionManager { private get; set; }

    public CombatController CreatePlayerCombatCharacter(Sprite sprite, List<PlayerAbilityData> playerAbilities)
    {
        var go = CreateGameObject(sprite);

        //Seems like we'll need some passthrough data for this...
        var character = CreateCharacter(Faction.Player, go);
        go.GetComponentInChildren<CharacterMouseInput>().owner = character;
        character.IsInMelee = Random.value > 0.5f;

        DesertContext.QuickBind(character);

        var controller = CreateController(go);
        HookCharacterIntoController(controller, character, playerAbilities);
        SetupHealthVisuals(character, go);
        controller.Init();

        DesertContext.FinishQuickBind<Character>();

        return controller;
    }

    GameObject CreateGameObject(Sprite art)
    {
        var prefab = CombatReferences.Get().combatCharacterPrefab;
        var go = GameObject.Instantiate(prefab) as GameObject;
        go.name = "Player";
        go.GetComponent<SpriteRenderer>().sprite = art;
        return go;
    }

    CombatController CreateController(GameObject go)
    {
        var controller = DesertContext.StrangeNew<CombatController>();
        controller.artGO = go;

        return controller;
    }

    //TODO: I'm putting all sortsa magic numbers up in this bitch for now.
    Character CreateCharacter(Faction f, GameObject artGO)
    {
        var character = DesertContext.StrangeNew<Character>();
        character.Setup(20);
        character.ownerGO = artGO;
        character.attackModule = CreateAttackModule();
        character.defenseModule = CreateDefenseModule();
        //TODO: this should include characters name...
        character.displayName = "<color=Orange>" + "PLAYA" + "</color>";
        character.myFaction = f;
        character.speed = 10;
        factionManager.Register(character);

        return character;
    }

    //TODO: I'm putting all sortsa magic numbers up in this bitch for now.
    AttackModule CreateAttackModule()
    {
        var attackModule = new AttackModule();
        attackModule.minDamage = 10;
        attackModule.maxDamage = 12;

        return attackModule;
    }

    //TODO: I'm putting all sortsa magic numbers up in this bitch for now.
    DefenseModule CreateDefenseModule()
    {
        var defenseModule = new DefenseModule();
        defenseModule.defenseValue = 0;
        defenseModule.damageReduction = 0;

        return defenseModule;
    }

    void HookCharacterIntoController(CombatController controller, Character character, List<PlayerAbilityData> playerAbilities)
    {
        controller.KilledEvent += () => factionManager.Unregister(character);
        controller.character = character;
        var combatActor = DesertContext.StrangeNew<PlayerCombatActor>();
        combatActor.playerAbilities = playerAbilities.ConvertAll(a => a.Create(controller));
        combatActor.Setup();
        controller.combatActor = combatActor;
    }

    void SetupHealthVisuals(Character enemyCharacter, GameObject go)
    {
        var dooberHelper = DesertContext.StrangeNew<CombatDamageDooberHelper>();
        dooberHelper.Setup(enemyCharacter.health, go);

        DesertContext.QuickBind(enemyCharacter.health);
        var healthDisplayGO = GameObject.Instantiate(PrefabGetter.healthDisplayPrefab) as GameObject;
        DesertContext.FinishQuickBind<Health>();

        healthDisplayGO.transform.SetParent(go.transform, true);
        healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
}
