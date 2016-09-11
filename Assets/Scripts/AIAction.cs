public interface AIAction {
	int GetActionWeight();
	void PerformAction(System.Action callback); 
}