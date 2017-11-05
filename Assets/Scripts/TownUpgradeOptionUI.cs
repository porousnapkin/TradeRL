using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TownUpgradeOptionUI : MonoBehaviour {
    public TextMeshProUGUI storyDescription;
    public TextMeshProUGUI gameDescription;
    public Button myButton;
    private System.Action callback;

    public void Setup(TownUpgradeOptionData data, System.Action clickCallback)
    {
        storyDescription.text = data.storyDescription;
        gameDescription.text = data.gameDescription;
        this.callback = clickCallback;

        myButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        myButton.onClick.RemoveListener(Clicked);
        callback();
    }
}
