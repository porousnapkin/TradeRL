using UnityEngine;
using UnityEngine.UI;

public class TeammateInfoPanel : MonoBehaviour {
    public PlayerTeam.TeammateData teammate;
    public Text nameText;
    public Text hpText;
    public GameObject woundedSignifier;
    public Image art;

	void Start () {
        teammate.character.health.HealthChangedEvent += HealthChanged;
        HealthChanged();
        nameText.text = teammate.character.displayName;
        woundedSignifier.SetActive(false);
        art.sprite = teammate.data.visuals;
	}

    private void HealthChanged()
    {
        hpText.text = "HP: " + teammate.character.health.Value + "/" + teammate.character.health.MaxValue;

        //TODO: Better way to deal with wounded?
        woundedSignifier.SetActive(teammate.character.health.Value <= 0);
    }
}
