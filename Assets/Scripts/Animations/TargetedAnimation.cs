using UnityEngine;

public interface TargetedAnimation {
	void Play(Character target, System.Action finished, System.Action activated);
}