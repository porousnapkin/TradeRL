public abstract class CombatAIConditional {
    public bool not = false;

    public bool Passes()
    {
        if (not)
            return !Check();
        return Check();
    }

    protected abstract bool Check();
}
