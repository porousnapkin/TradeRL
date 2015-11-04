public static class GlobalEvents {
	public static System.Action<Town> LocationPickedEvent = delegate{};
	public static System.Action<int, TradeGood, Town> GoodsSoldEvent = delegate{};
	public static System.Action<Town> TownLeveldUpEvent = delegate{};
}