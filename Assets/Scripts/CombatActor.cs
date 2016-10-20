public interface CombatActor {
    void Act(System.Action callback);
    void Cleanup();
}
