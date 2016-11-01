using UnityEngine;

public class PremadeCharacterData : ScriptableObject
{
    public CharacterCreationDataBlob background;
    public CharacterCreationDataBlob advantage;
    public CharacterCreationDataBlob twist;

    public void Setup()
    {
        background.Apply();
        advantage.Apply();
        twist.Apply();
    }
}

