using UnityEngine;

[ExecuteInEditMode]
public class CopyOtherRectTransformValues : MonoBehaviour {
    public RectTransform toCopy;
    public RectTransform myTransform;

	void Update () {
        myTransform.anchoredPosition = toCopy.anchoredPosition;
        myTransform.anchorMax = toCopy.anchorMax;
        myTransform.anchorMin = toCopy.anchorMin;
        myTransform.offsetMax = toCopy.offsetMax;
        myTransform.offsetMin = toCopy.offsetMin;
        myTransform.pivot = toCopy.pivot;
	}
}
