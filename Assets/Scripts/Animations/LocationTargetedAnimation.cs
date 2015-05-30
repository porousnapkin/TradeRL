using UnityEngine;

public interface LocationTargetedAnimation {
	void Play(Vector2 target, System.Action finished, System.Action activated);
}