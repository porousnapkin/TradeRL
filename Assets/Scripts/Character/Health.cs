public class Health {
	int health = 0;	
	public int Value { 
		get {
			return health;
		}
		set {
			health = value;
			HealthChangedEvent(value);
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

	public void Damage(int damage) {
		Value -= damage;
	}

	public void Heal(int amount) {
		Value += amount;
	}
}