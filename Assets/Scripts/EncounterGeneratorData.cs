using System.Collections.Generic;
using UnityEngine;

public class EncounterGeneratorData : ScriptableObject
{
    public static EncounterGeneratorData Instance
    {
        get
        {
            return Resources.Load("EncounterGeneratorData") as EncounterGeneratorData;
        }
    }

    [System.Serializable]
    public class FactionMembers
    {
        public EncounterFaction faction;
        public List<AICharacterData> members;
    }

    public List<FactionMembers> members = new List<FactionMembers>();
}

