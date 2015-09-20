using UnityEngine;
using UnityEngine.UI;

public class CityActionDisplay : MonoBehaviour {
	public Button backButton;
		
	public void SetReturnGameObject(GameObject go) {
		backButton.onClick.AddListener(() => go.SetActive(true));
		backButton.onClick.AddListener(() => gameObject.SetActive(false));
	}
}