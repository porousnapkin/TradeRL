using UnityEngine;

public class HealthDoober : MonoBehaviour {
	const float raiseHeight = 0.25f;
	const float raiseTime = 0.25f;
	const float lingerTime = 0.5f;

	void Start() {
		LeanTween.move(gameObject, transform.position + new Vector3(0, raiseHeight, 0), raiseTime).setEase(LeanTweenType.easeOutQuart).
			setOnComplete(() => Invoke("Finished", lingerTime));
	}

	void Finished() {
		GameObject.Destroy(gameObject);	
	}
}