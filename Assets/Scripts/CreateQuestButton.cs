using UnityEngine.UI;
using UnityEngine;
using strange.extensions.mediation.impl;
using System;

public class CreateQuestButton : DesertView {
    public Button button;
    public Town town;
    public LocationFactory locationFactory;
    public LocationData questLocationData;
    public QuestData quest;
    public event System.Action<QuestData> startThisQuestPlease;

    protected override void Start()
    {
        button.onClick.AddListener(CreateQuest);
    }

    void CreateQuest()
    {
        if (town == null)
            Debug.LogError("No town, this will break");
        if (locationFactory == null)
            Debug.LogError("No location factory, this will break");
        if(quest == null)
            Debug.LogError("No quest data, this will break");

        Debug.Log("Creating quest");

        startThisQuestPlease(quest);

        var pos = town.worldPosition;
        locationFactory.CreateALocationNearAPoint(questLocationData, (int)pos.x, (int)pos.y, 3, 6);
    }
}


public class CreateQuestButtonMediator: Mediator
{
    [Inject]
    public CreateQuestButton view { private get; set; }
    [Inject]
    public LocationFactory locationFactory { private get; set; }
    [Inject]
    public Quests quests { private get; set; }

    public override void OnRegister()
    {
        view.locationFactory = locationFactory;
        view.startThisQuestPlease += StartQuest;
    }

    public override void OnRemove()
    {
        view.startThisQuestPlease -= StartQuest;
    }

    void StartQuest(QuestData data)
    {
        quests.AddActiveQuest(data.Create());
    }
}
