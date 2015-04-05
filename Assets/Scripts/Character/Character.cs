public class Character {
	public Health health;

	public Character() {
		health = new Health();
		health.MaxValue = 50;
		health.Value = health.MaxValue;
	}
}