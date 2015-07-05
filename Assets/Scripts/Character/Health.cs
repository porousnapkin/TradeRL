using UnityEngine;

public class Health {
	int health = 0;	
	public int Value { 
		get {
			return health;
		}
		set {
			health = Mathf.Max(0, Mathf.Min(value, maxHealth));
			HealthChangedEvent(health);
		}
	}
	public event System.Action<int> HealthChangedEvent = delegate{};
	int maxHealth = 10;
	public int MaxValue {
		get {
			return maxHealth;
		}	
		set {
			maxHealth = value;
			MaxHealthChangedEvent(value);
		}
	}
	public event System.Action<int> MaxHealthChangedEvent = delegate{};
	public event System.Action<int> DamagedEvent = delegate{};
	public event System.Action KilledEvent = delegate{};
	public event System.Action<int> HealedEvent = delegate{};

	public void Damage(int damage) {
		Value -= damage;
		if(Value <= 0)
			KilledEvent();
		else
			DamagedEvent(damage);
	}

	public void Heal(int amount) {
		Value += amount;
		HealedEvent(amount);
	}

	public void Kill() {
		health = 0;
		KilledEvent();
	}
}