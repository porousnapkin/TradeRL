using System.Collections.Generic;

public class PlayerTeam {
    public class TeammateData
    {
        public AICharacterData data;
        public Character character;
    }
    List<TeammateData> allies = new List<TeammateData>();
    AICharacterFactory aiFactory;
    public event System.Action TeamUpdatedEvent = delegate { };

    [PostConstruct]
    public void PostConstruct()
    {
        aiFactory = DesertContext.StrangeNew<AICharacterFactory>();
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
        teammate.character.health.KilledEvent += () => RemoveAlly(teammate);

        allies.Add(teammate);

        TeamUpdatedEvent();
    }

    void RemoveAlly(TeammateData ally)
    {
        allies.Remove(ally);

        TeamUpdatedEvent();
    }

    public List<Character> GetTeamCharacters()
    {
        return allies.ConvertAll(a => a.character);
    }

    public List<TeammateData> GetTeammateData()
    {
        return allies;
    }
}
