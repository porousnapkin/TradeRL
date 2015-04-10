using UnityEngine;

public class Character {
	public Health health;
	public Vector2 WorldPosition { get; set; }

	public Character() {
		health = new Health();
		health.MaxValue = 50;
		health.Value = health.MaxValue;
	}
}