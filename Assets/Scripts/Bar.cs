using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    public RectTransform barTransform;
    float percent;

    public void SetPercent(float percent) {
        this.percent = percent;

        barTransform.localScale = new Vector3(percent, 1, 1);
	}
}
