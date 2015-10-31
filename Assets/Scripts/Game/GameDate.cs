using UnityEngine;
using System.Collections;

public class GameDate {
	int days = 0;
	public int Days { get { return days; } }
	public event System.Action<int> DaysPassedEvent = delegate{};

	public void AdvanceDays(int daysPassed) {
		days += daysPassed;

		DaysPassedEvent(daysPassed);
	}
}
