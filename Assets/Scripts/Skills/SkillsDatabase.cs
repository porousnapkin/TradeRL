using UnityEngine;
using System.Collections.Generic;

public class SkillsDatabase : ScriptableObject {
    public static SkillsDatabase Instance
    {
        get
        {
            return Resources.Load("SkillsDatabase") as SkillsDatabase;
        }
    }

    public List<SkillData> allSkills;
}
