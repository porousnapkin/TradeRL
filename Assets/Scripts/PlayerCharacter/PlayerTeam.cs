using System.Collections.Generic;

public class PlayerTeam {
    List<AICharacterData> playerAllies = new List<AICharacterData>();

    public List<CombatController> CreateCombatAllies()
    {
        List<CombatController> allies = new List<CombatController>();

        var aiFactory = DesertContext.StrangeNew<AICharacterFactory>();
        for(int i = 0; i < playerAllies.Count; i++)
        {
            var controller = aiFactory.CreateAICharacter(playerAllies[i], Faction.Player);
            allies.Add(controller);
        }

        return allies;
    }

    public void AddAlly(AICharacterData allyData)
    {
        playerAllies.Add(allyData);
    }
}
