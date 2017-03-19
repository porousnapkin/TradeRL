using System.Linq;
using System.Collections.Generic;

public class RandomEncounterGenerator
{
    const int maxEncounterSize = 8;
    const int maxGenerationAttempts = 10;
    Dictionary<EncounterFaction, List<AICharacterData>> factionToCharacters = new Dictionary<EncounterFaction, List<AICharacterData>>();

    [PostConstruct]
    public void PostConstruct()
    {
        var data = EncounterGeneratorData.Instance;

        data.members.ForEach(e => factionToCharacters[e.faction] = e.members);
    }

    public List<AICharacterData> CreateEncounterGroupForFactions(List<EncounterFaction> factions, int encounterCost)
    {
        var allCharacters = new List<AICharacterData>();
        factions.ForEach(f => allCharacters.AddRange(factionToCharacters[f]));
        allCharacters.Distinct();

        List<AICharacterData> group = new List<AICharacterData>();
        for(int i = 0; i < maxGenerationAttempts; i++)
        {
            group = GenerateGroup(encounterCost, allCharacters);
            if (group.Count <= maxEncounterSize)
                return group;
        }

        return group;
    }

    List<AICharacterData> GenerateGroup(int encounterCost, List<AICharacterData> allCharacters)
    {
        var group = new List<AICharacterData>();
        while (encounterCost > 0)
        {
            var appropriateCharacters = GetCharactersForEncounterCost(encounterCost, allCharacters);
            if (appropriateCharacters.Count == 0)
                return group;

            var newCharacter = GetNewCharacterForEncounter(encounterCost, appropriateCharacters);
            group.Add(newCharacter);
            encounterCost -= newCharacter.encounterPickerWeight;
        }

        return group;
    }

    List<AICharacterData> GetCharactersForEncounterCost(int encounterCost, List<AICharacterData> allCharacters)
    {
        var appropriateCharcters = new List<AICharacterData>(allCharacters);
        appropriateCharcters.RemoveAll(a => a.encounterPickerWeight > encounterCost);
        return appropriateCharcters;
    }

    AICharacterData GetNewCharacterForEncounter(int encounterCost, List<AICharacterData> appropriateCharacters)
    {
        var indexedWeights = appropriateCharacters.ConvertAll(a => a.encounterPickerWeight);
        var index = WeightedRandomPicker.PickRandomWeightedIndex(indexedWeights);
        return appropriateCharacters[index];
    }
}

