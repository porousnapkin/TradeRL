using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUpgradeOptionData : ScriptableObject {
    public string storyDescription;
    public string gameDescription;
    public List<TownBenefitData> benefits = new List<TownBenefitData>();

    public void Apply(Town t)
    {
        benefits.ForEach(b =>
        {
            b.Create(t).Apply();
        });
    }
}
