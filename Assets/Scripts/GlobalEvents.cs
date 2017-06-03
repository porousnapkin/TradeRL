public static class GlobalEvents {
	public static System.Action GameSetupEvent = delegate{};
	public static System.Action GameBeganEvent = delegate{};
	public static System.Action<Town> LocationPickedEvent = delegate{};
	public static System.Action<int, TradeGood, Town> GoodsSoldEvent = delegate{};
	public static System.Action<int, Town> GoodsPurchasedEvent = delegate{};
	public static System.Action<Town> TownEconomyLeveldUpEvent = delegate{};
	public static System.Action<Town, Building> BuildingBuilt = delegate{};
	public static System.Action<Town> TownDiscovered = delegate{};
    public static System.Action CombatStarted = delegate { };
    public static System.Action CombatEnded = delegate { };
    public static System.Action<Character> CombatantTurnStart = delegate { };
    public static System.Action ExpeditionBegan = delegate { };
    public static System.Action ExpeditionEnded = delegate { };
}