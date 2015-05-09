using UnityEngine;

public interface InputTargetFilter {
	bool PassesFilter(Vector2 position);
}