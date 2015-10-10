public static class GlobalEvents {
	public static event System.Action<Town> LocationPickedEvent = delegate{};
	public static void LocationPicked(Town t) { LocationPickedEvent(t); }
}