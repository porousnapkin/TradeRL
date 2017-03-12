using UnityEngine;
using UnityEngine.UI;

public class TeammateInfoPanel : MonoBehaviour {
    public PlayerTeam.TeammateData teammate;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI hpText;
    public GameObject woundedSignifier;
    public Image art;
    public UIImageRaycasterPopup popup;
    int popupSpace;

	void Start () {
        popupSpace = popup.ReserveSpace();

        teammate.character.health.HealthChangedEvent += HealthChanged;
        HealthChanged();
        nameText.text = teammate.character.displayName;
        woundedSignifier.SetActive(false);
        art.sprite = teammate.data.visuals;
	}

    void HealthChanged()
    {
        hpText.text = "HP: " + teammate.character.health.Value + "/" + teammate.character.health.MaxValue;

        var isWounded = teammate.character.health.Value <= 0;
        woundedSignifier.SetActive(isWounded);

        if (isWounded)
            popup.Record("Wounded allies don't participate\nin combat until healed in town.", popupSpace);
        else
            popup.Record("", popupSpace);
    }
}
