using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownEventTextDisplay : MonoBehaviour {
    public Button button;
    public TextMeshProUGUI text;
    public UIImageRaycasterPopup popup;

    public void Setup(string description, string popupText)
    {
        text.text = description;
        popup.Record(popupText);

        button.onClick.AddListener(() => GameObject.Destroy(gameObject));
    }
}
