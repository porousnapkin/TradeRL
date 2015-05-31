using UnityEngine;

public class Effort {
	int effort = 10;	
	public int Value { 
		get {
			return effort;
		}
		set {
			effort = Mathf.Max(Mathf.Min(maxEffort, value), 0);
			EffortChangedEvent(effort);
		}
	}
	public event System.Action<int> EffortChangedEvent = delegate{};
	int maxEffort = 10;
	public int MaxValue {
		get {
			return maxEffort;
		}	
		set {
			maxEffort = value;
			MaxEffortChangedEvent(maxEffort);
		}
	}
	public event System.Action<int> MaxEffortChangedEvent = delegate{};
	public event System.Action<int> EffortSpentEvent = delegate{};

	public void Spend(int amount) {
		Value -= amount;
		EffortSpentEvent(amount);
	}
}