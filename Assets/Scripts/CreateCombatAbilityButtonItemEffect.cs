using System;

public class CreateCombatAbilityButtonItemEffect : ItemEffect 
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
	public PlayerAbilityData ability { private get; set; }

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
            playerCharacter.AddCombatPlayerAbility(ability);
        else if(numWas > 0 && newNum <= 0)
            playerCharacter.RemoveCombatPlayerAbility(ability);
    }
}

