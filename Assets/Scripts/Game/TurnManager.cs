using System.Collections.Generic;

public class TurnManager {
	List<CombatController> playerControllers = new List<CombatController>();	
	List<CombatController> enemyControllers = new List<CombatController>();	
	bool playersTurn = true;
	int finishedActors = 0;

	public event System.Action TurnEndedEvent = delegate {};

	public void Register(CombatController c) {
		if(c.GetCharacter().myFaction == Faction.Player)
			RegisterPlayer(c);
		else
			RegisterEnemy(c);
	}

	public void Unregister(CombatController c) {
		if(c.GetCharacter().myFaction == Faction.Player)
			UnregisterPlayer(c);
		else
			UnregisterEnemy(c);
	}

	public void RegisterPlayer(CombatController player) {
		playerControllers.Add(player);
		if(playersTurn)
			player.BeginTurn(PlayerTurnFinished);
	}

	public void UnregisterPlayer(CombatController player) {
		playerControllers.Remove(player);
		if(playersTurn && finishedActors >= playerControllers.Count)
			BeginEnemyTurn();
	}

	public void RegisterEnemy(CombatController enemy) {
		enemyControllers.Add(enemy);
		if(!playersTurn)
			enemy.BeginTurn(EnemyTurnFinished);
	}

	public void UnregisterEnemy(CombatController enemy) {
		enemyControllers.Remove(enemy);
		if(!playersTurn && finishedActors >= enemyControllers.Count) {
			TurnEndedEvent();	
			BeginPlayerTurn();
		}
	}

	public void BeginPlayerTurn() {
		finishedActors = 0;
		playersTurn = true;
		foreach(var controller in playerControllers)
			controller.BeginTurn(PlayerTurnFinished);
	}

	void PlayerTurnFinished() {
		finishedActors++;
		if(finishedActors >= playerControllers.Count)
			BeginEnemyTurn();
	}

	public void BeginEnemyTurn() {
		finishedActors = 0;
		playersTurn = false;
		foreach(var controller in enemyControllers)
			controller.BeginTurn(EnemyTurnFinished);	

		if(enemyControllers.Count == 0) {
			TurnEndedEvent();
			BeginPlayerTurn();
		}
	}

	void EnemyTurnFinished() {
		finishedActors++;
		if(finishedActors >= enemyControllers.Count) {
			TurnEndedEvent();
			BeginPlayerTurn();
		}
	}

}