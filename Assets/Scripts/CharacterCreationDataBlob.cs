using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationDataBlob : ScriptableObject {
    public enum CharacterCreationDataType
    {
        Background,
        Advantage,
        Twist,
    }

    public CharacterCreationDataType type;
    public string displayName;
    public string displayDescription;
    public List<CharacterCreationSkill> skills;
    public List<AICharacterData> startingAllies;
    public List<ItemData> startingItems;

    public void Apply()
    {
        var helper = DesertContext.StrangeNew<CharacterCreationDataHelper>();
        helper.Apply(this);
    }
}

public class CharacterCreationDataHelper
{
    [Inject]
    public PlayerTeam playerTeam { private get; set; }
    [Inject]
    public Inventory inventory { private get; set; }
    [Inject]
    public PlayerSkills playerSkills { private get; set; }
    [Inject]
    public PlayerCharacter playerCharacter { private get; set; }

    public void Apply(CharacterCreationDataBlob blob)
    {
        var character = playerCharacter.GetCharacter();
        blob.startingAllies.ForEach(a => playerTeam.AddAlly(a, true));
        blob.startingItems.ForEach(i => inventory.AddItem(i.Create(character )));
        blob.skills.ForEach(s =>
        {
            var activeSkill = playerSkills.GetSkill(s.skill);
            for(int i = 0; i < s.level; i++)
                activeSkill.LevelUp();
        });
    }
}

