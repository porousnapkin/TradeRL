using System.Collections.Generic;

public class FactionManager {
	HashSet<Character> playerFaction = new HashSet<Character>();	
	public List<Character> PlayerMembers { get { return new List<Character>(playerFaction); }}
    HashSet<Character> enemyFaction = new HashSet<Character>();
	public List<Character> EnemyMembers { get { return new List<Character>(enemyFaction); }}

	public void Register(Character c) {
		if(c.myFaction == Faction.Player)
			RegisterToPlayerFaction(c);	
		else
			RegisterToEnemyFaction(c);
	}

	public void Unregister(Character c) {
		if(c.myFaction == Faction.Player)
			UnregisterToPlayerFaction(c);	
		else
			UnregisterToEnemyFaction(c);
	}

	public List<Character> GetOpponents(Character c) {
		if(c.myFaction == Faction.Player)
			return new List<Character>(EnemyMembers);
		else
			return new List<Character>(PlayerMembers);
	}

    public List<Character> GetAllies(Character c)
    {
        if(c.myFaction != Faction.Player)
			return new List<Character>(EnemyMembers);
		else
			return new List<Character>(PlayerMembers);
    }

	public void RegisterToPlayerFaction(Character c) {
		playerFaction.Add(c);
	}

	public void UnregisterToPlayerFaction(Character c) {
		playerFaction.Remove(c);
	}

	public void RegisterToEnemyFaction(Character c) {
		enemyFaction.Add(c);	
	}

	public void UnregisterToEnemyFaction(Character c) {
		enemyFaction.Remove(c);	
	}
}