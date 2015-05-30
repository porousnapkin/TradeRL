using UnityEngine;

public interface LocationTargetedAnimation {
	void Play(Character owner, Vector2 target, System.Action finished, System.Action activated);
}