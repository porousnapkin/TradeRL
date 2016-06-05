using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatTester : MonoBehaviour {
    public GameObject combatViewPrefab;
    public CombatEncounterData encounterData;

    void Start()
    {
        var go = GameObject.Instantiate(combatViewPrefab) as GameObject;
        go.transform.SetParent(transform);
        var combatView = go.GetComponent<CombatView>();
        List<CombatController> enemies = new List<CombatController>();
        List<CombatController> allies = new List<CombatController>();

        var factory = DesertContext.StrangeNew<AICharacterFactory>();
        for(int i = 0; i < encounterData.characters.Count; i++)
        {
            var controller = factory.CreateAICharacter(encounterData.characters[i], Faction.Enemy);
            enemies.Add(controller);
            controller.GetCharacter().health.KilledEvent += () => enemies.Remove(controller);
        }
        CombatView.PositionEnemyCharacters(enemies);
        for(int i = 0; i < encounterData.characters.Count-1; i++)
        {
            var controller = factory.CreateAICharacter(encounterData.characters[i], Faction.Player);
            allies.Add(controller);
            controller.GetCharacter().health.KilledEvent += () => allies.Remove(controller);
        }
        CombatView.PositionPlayerCharacters(allies);

        StartCoroutine(DebugCombat(enemies, allies));
    }

    IEnumerator DebugCombat(List<CombatController> enemies, List<CombatController> allies)
    {
        yield return new WaitForSeconds(0.5f);

        while(enemies.Count > 0 && allies.Count > 0)
        {
            foreach(var enemy in enemies)
            {
                enemy.Attack(allies[Random.Range(0, allies.Count)].GetCharacter(), () => { });
                yield return new WaitForSeconds(0.5f);
            }

            foreach(var ally in allies)
            {
                ally.Attack(enemies[Random.Range(0, enemies.Count)].GetCharacter(), () => { });
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        enemies[0].Attack(allies[0].GetCharacter(), () => { Debug.Log("Attack finished"); });
    }
}
