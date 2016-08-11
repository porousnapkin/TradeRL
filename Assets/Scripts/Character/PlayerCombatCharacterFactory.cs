using UnityEngine;

public class PlayerCombatCharacterFactory {
    [Inject] public FactionManager factionManager { private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public CombatController CreatePlayerCombatCharacter()
    {
        var go = CreateGameObject();

        var character = CreateCharacter(go);
        go.GetComponentInChildren<CharacterMouseInput>().owner = character;
        //TODO: This should be a player set value.
        character.IsInMelee = true;

        DesertContext.QuickBind(character);

        var controller = CreateController(go);
        HookCharacterIntoController(controller, character);
        SetupHealthVisuals(character, go);
        controller.Init();

        DesertContext.FinishQuickBind<Character>();

        return controller;
    }

    GameObject CreateGameObject()
    {
        var prefab = CombatReferences.Get().combatCharacterPrefab;
        var go = GameObject.Instantiate(prefab) as GameObject;
        go.name = "Player";
        go.GetComponent<SpriteRenderer>().sprite = playerCharacter.GetArt();
        return go;
    }

    CombatController CreateController(GameObject go)
    {
        var controller = DesertContext.StrangeNew<CombatController>();
        controller.artGO = go;

        return controller;
    }

    Character CreateCharacter(GameObject artGO)
    {
        var character = playerCharacter.GetCharacter();
        character.ownerGO = artGO;
        factionManager.Register(character);

        return character;
    }


    void HookCharacterIntoController(CombatController controller, Character character)
    {
        controller.KilledEvent += () => factionManager.Unregister(character);
        controller.character = character;
        var combatActor = DesertContext.StrangeNew<PlayerCombatActor>();
        combatActor.playerAbilities = playerCharacter.GetCombatAbilities().ConvertAll(a => a.Create(controller));
		var modifiers = DesertContext.StrangeNew<ActivePlayerAbilityModifiers>();
		modifiers.allAvailableAbilityModifiers = playerCharacter.GetCombatAbilityModifiers().ConvertAll(a => a.Create(controller));
		modifiers.owner = controller;
		combatActor.abilityModifiers = modifiers;
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
