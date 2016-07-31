using UnityEngine;

public class PlayerCombatCharacterFactory {
    [Inject] public FactionManager factionManager { private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public CombatController CreatePlayerCombatCharacter(Sprite sprite)
    {
        var go = CreateGameObject(sprite);

        var character = CreateCharacter(go);
        go.GetComponentInChildren<CharacterMouseInput>().owner = character;
        character.IsInMelee = Random.value > 0.5f;

        DesertContext.QuickBind(character);

        var controller = CreateController(go);
        HookCharacterIntoController(controller, character);
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
