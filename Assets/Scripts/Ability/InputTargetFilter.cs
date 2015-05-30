using UnityEngine;

public interface InputTargetFilter {
	bool PassesFilter(Character owner, Vector2 position);
}