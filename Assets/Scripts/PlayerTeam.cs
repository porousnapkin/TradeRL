using System.Collections.Generic;

public class PlayerTeam {
    public class TeammateData
    {
        public AICharacterData data;
        public Character character;
    }
    [Inject] public StoryFactory storyFactory { private get; set; }
    List<TeammateData> allies = new List<TeammateData>();
    List<TeammateData> alliesWaitingToStabilize = new List<TeammateData>();
    AICharacterFactory aiFactory;
    public event System.Action TeamUpdatedEvent = delegate { };

    [PostConstruct]
    public void PostConstruct()
    {
        aiFactory = DesertContext.StrangeNew<AICharacterFactory>();

        GlobalEvents.CombatEnded += ShowNextWoundedStory;
    }

    ~PlayerTeam()
    {
        GlobalEvents.CombatEnded -= ShowNextWoundedStory;
    }

    public List<CombatController> GetCombatAlliesControllers()
    {
        List<CombatController> output = new List<CombatController>();

        for(int i = 0; i < allies.Count; i++)
        {
            if (allies[i].character.health.Value > 0)
            {
                var controller = aiFactory.CreateCombatController(allies[i].character, allies[i].data, Faction.Player);
                output.Add(controller);
            }
        }

        return output;
    }

    public void AddAlly(AICharacterData allyData)
    {
        var teammate = new TeammateData();
        teammate.data = allyData;
        teammate.character = aiFactory.CreateCharacter(allyData, Faction.Player);
        teammate.character.health.KilledEvent += () => AllyWounded(teammate);

        allies.Add(teammate);

        TeamUpdatedEvent();
    }

    private void AllyWounded(TeammateData teammate)
    {
        alliesWaitingToStabilize.Add(teammate);
    }

    private void ShowNextWoundedStory()
    {
        if (alliesWaitingToStabilize.Count == 0)
            return;

        var storyVisuals = storyFactory.CreateStory(SpecialCaseStories.Instance.allyWoundedStory, ShowNextWoundedStory);
        storyVisuals.AddSpecialCaseString("Ally", alliesWaitingToStabilize[0].character.displayName);
    }

    public void RemoveAlly(Character character)
    {
        allies.Remove(allies.Find(a => a.character == character));

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

    public TeammateData GetATeammateReadyToStabilize()
    {
        return alliesWaitingToStabilize[0];
    }

    public void TeammateStabilized(TeammateData teammate)
    {
        alliesWaitingToStabilize.Remove(teammate);
    }

    public void TeammateFailedToStabilize(TeammateData teammate)
    {
        alliesWaitingToStabilize.Remove(teammate);
        RemoveAlly(teammate);
    }

    void RemoveAlly(TeammateData ally)
    {
        allies.Remove(ally);

        TeamUpdatedEvent();
    }
}
