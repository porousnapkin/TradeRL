using UnityEngine;

public class IgnoreFacingScaling : MonoBehaviour {
	public void Update() {
		if(transform.lossyScale.x < 0)
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
	}
}