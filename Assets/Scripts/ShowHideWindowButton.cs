using UnityEngine;
using UnityEngine.UI;

public class ShowHideWindowButton : MonoBehaviour {
    public GameObject window;
    public Button button;

	void Start () {
        button.onClick.AddListener(ShowHideWindow);
	}

    private void ShowHideWindow()
    {
        window.SetActive(!window.activeSelf);
    }
}
