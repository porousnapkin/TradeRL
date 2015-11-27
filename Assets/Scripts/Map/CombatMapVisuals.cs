using UnityEngine;
using System.Collections;

public class CombatMapVisuals : MonoBehaviour {
	public float scaleTime = 1.0f;

	void Start() {
		transform.localScale = new Vector3(1, 0);
		LeanTween.value(gameObject, (v) => transform.localScale = new Vector3(1, v, 1), 0, 1, scaleTime);
	}
}
