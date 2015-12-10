using UnityEngine;
using System.Collections;

public class CombatMapVisuals : MonoBehaviour {
	public float scaleTime = 1.0f;
	public GridInputCollector inputCollector;

	void Start() {
//		transform.localScale = new Vector3(1, 0);
//		inputCollector.OverrideInput((v) => Debug.Log ("Hit " + v));
	}

	public void Show() {
		transform.localScale = new Vector3(1, 0);
		LeanTween.value(gameObject, (v) => transform.localScale = new Vector3(1, v, 1), 0, 1, scaleTime);
	}

	public void Finish () {
		LeanTween.value(gameObject, (v) => transform.localScale = new Vector3(1, v, 1), 1, 0, scaleTime);
	}
}
