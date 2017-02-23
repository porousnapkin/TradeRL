using UnityEngine;

public class SpecialCaseStories : ScriptableObject
{
    public static SpecialCaseStories Instance
    {
        get
        {
            return Resources.Load("SpecialCaseStories") as SpecialCaseStories;
        }
    }

    public StoryData allyWoundedStory;
}

