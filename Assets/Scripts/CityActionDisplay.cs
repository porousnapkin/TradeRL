using UnityEngine;
using UnityEngine.UI;

public class CityActionDisplay : DesertView {
	public Button backButton;
		
	public void SetReturnGameObject(GameObject go) {
        if (backButton == null)
            return;
		backButton.onClick.AddListener(() => go.SetActive(true));
		backButton.onClick.AddListener(() => gameObject.SetActive(false));
	}
}