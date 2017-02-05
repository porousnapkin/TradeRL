using System;

public class CreateCombatAbilityButtonItemEffect : ItemEffect 
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
	public PlayerAbilityData ability { private get; set; }
    private bool preppedToRemove = false;
    private bool hasBeenAdded = false;

    [PostConstruct]
    public void PostConstruct()
    {
        GlobalEvents.CombatEnded += CombatEnded;
    }

    ~CreateCombatAbilityButtonItemEffect()
    {
        GlobalEvents.CombatEnded -= CombatEnded;
    }

    private void CombatEnded()
    {
        if(preppedToRemove)
            playerCharacter.RemoveCombatPlayerAbility(ability);

        preppedToRemove = false;
        hasBeenAdded = false;
    }

    public void Activate()
    {
    }

    public bool CanActivate()
    {
        return false;
    }

    public void NumItemsChanged(int numWas, int newNum)
    {
        if (numWas <= 0 && newNum > 0)
            TryToAdd();
        else if (numWas > 0 && newNum <= 0)
            preppedToRemove = true;
    }

    private void TryToAdd()
    {
        if (preppedToRemove)
            preppedToRemove = false;

        if (hasBeenAdded)
            return;

        playerCharacter.AddCombatPlayerAbility(ability);
        hasBeenAdded = true;
    }
}

