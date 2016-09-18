using System.Collections.Generic;

public class PlayerTeam {
    class TeammateData
    {
        public AICharacterData data;
        public Character character;
    }
    List<TeammateData> allies = new List<TeammateData>();
    AICharacterFactory aiFactory;

    [PostConstruct]
    public void PostConstruct()
    {
        aiFactory = DesertContext.StrangeNew<AICharacterFactory>();
        BasePlayerCharacterStats.Instance.defaultAllies.ForEach(a => AddAlly(a));
    }

    public List<CombatController> GetCombatAlliesControllers()
    {
        List<CombatController> output = new List<CombatController>();

        for(int i = 0; i < allies.Count; i++)
        {
            var controller = aiFactory.CreateCombatController(allies[i].character, allies[i].data, Faction.Player);
            output.Add(controller);
        }

        return output;
    }

    public void AddAlly(AICharacterData allyData)
    {
        var teammate = new TeammateData();
        teammate.data = allyData;
        teammate.character = aiFactory.CreateCharacter(allyData, Faction.Player);

        allies.Add(teammate);
    }
}
