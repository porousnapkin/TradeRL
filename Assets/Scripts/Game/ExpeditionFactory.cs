public class ExpeditionFactory {
	[Inject] public TravelingStorySpawner travelingStorySpawner {private get; set;}

	static Expedition activeExpedition = null;
	Expedition ActiveExpedition { get { return activeExpedition; } }

	public void BeginExpedition(Town destination) {
		activeExpedition = DesertContext.StrangeNew<Expedition>();

		activeExpedition.Begin(destination);

		travelingStorySpawner.BeginSpawning();
	}

	public void FinishExpedition() {
		if(activeExpedition != null) {
			activeExpedition.Finish();

			travelingStorySpawner.StopSpawning();
			travelingStorySpawner.ClearSpawns();
		}
	}
}
