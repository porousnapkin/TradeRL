using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TownActionButton : MonoBehaviour
{
    public GameObject newActionNotifier;
    public TextMeshProUGUI actionTitle;
    public Button button;

    void Awake()
    {
        newActionNotifier.SetActive(false);
    }

    public void Setup(string actionDescription, System.Action clickedCallback)
    {
        actionTitle.text = actionDescription;
        button.onClick.AddListener(() => clickedCallback());
    }

    public void SimulateButtonHit()
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerClickHandler);
    }

    public void NotifyNew()
    {
        newActionNotifier.SetActive(true);
    }

    public void StopNotifyingNew()
    {
        newActionNotifier.SetActive(false);
    }
}
