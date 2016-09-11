using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class DebugCharacterCreator : DesertView {
    public List<int> skillLevelsIndexed = new List<int>();
    public PlayerCharacter playerCharacter { private get; set; }
    public PlayerSkills skills { private get; set; }

    public void CreateCharacter()
    {
        var skillDatabase = SkillsDatabase.Instance;
        for(int i = 0; i < skillLevelsIndexed.Count; i++)
        {
            var skill = skills.GetSkill(skillDatabase.allSkills[i]);
            skill.SetLevel(skillLevelsIndexed[i]);
        }

        playerCharacter.BuildCharacter();
    }
}

public class DebugCharacterCreatorMediator : Mediator {
	[Inject] public DebugCharacterCreator view { private get; set; }
    [Inject] public PlayerCharacter pc { private get; set; }
    [Inject] public PlayerSkills skills { private get; set; }

	public override void OnRegister ()
	{
        view.playerCharacter = pc;
        view.skills = skills;
	}
}
