using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class ExtensionMethods {
	public static void RefireButton(MonoBehaviour monoBehaviour, Button button, System.Action action) {
		var buttonHelper = button.gameObject.GetComponent<ButtonHelper>();
		if(buttonHelper == null)
			buttonHelper = button.gameObject.AddComponent<ButtonHelper>();

		buttonHelper.pointerDownEvent += () => monoBehaviour.StartCoroutine(RefireButtonCoroutine(action));
		buttonHelper.pointerUpEvent += () => monoBehaviour.StopAllCoroutines();
	}

	static IEnumerator RefireButtonCoroutine(System.Action action) {
		action();

		yield return new WaitForSeconds(0.5f);

		while(true) {
			action();
			yield return new WaitForSeconds(0.05f);
		}
	}

    public static void SetLayerRecursively(this GameObject baseGO, int layer)
    {
        baseGO.layer = layer;

        foreach (Transform child in baseGO.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}