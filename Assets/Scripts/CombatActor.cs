public interface CombatActor {
    void SetupAction(System.Action callback);
    void Act(System.Action callback);
    void Cleanup();
}
