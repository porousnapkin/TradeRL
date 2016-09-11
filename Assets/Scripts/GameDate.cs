public class GameDate {
	int days = 0;
	public int Days { get { return days; } }
	public event System.Action<int> DaysPassedEvent = delegate{};

	bool hasADailyEventOccuredToday = false;
	public bool HasADailyEventOccuredToday { get { return hasADailyEventOccuredToday; }}

	public void AdvanceDays(int daysPassed) {
		hasADailyEventOccuredToday = false;
		days += daysPassed;

		DaysPassedEvent(daysPassed);
	}

	public void DailyEventOccured() {
		hasADailyEventOccuredToday = true;
	}
}
