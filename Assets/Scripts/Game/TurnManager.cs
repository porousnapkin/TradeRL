using System.Collections.Generic;

public class TurnManager {
	List<Controller> playerControllers = new List<Controller>();	
	List<Controller> enemyControllers = new List<Controller>();	
	bool playersTurn = true;
	int finishedActors = 0;

	public event System.Action TurnEndedEvent = delegate {};

	public void RegisterPlayer(Controller player) {
		playerControllers.Add(player);
		if(playersTurn)
			player.BeginTurn(PlayerTurnFinished);
	}

	public void UnregisterPlayer(Controller player) {
		playerControllers.Remove(player);
	}

	public void RegisterEnemy(Controller enemy) {
		enemyControllers.Add(enemy);
		if(!playersTurn)
			enemy.BeginTurn(EnemyTurnFinished);
	}

	public void UnregisterEnemy(Controller enemy) {
		enemyControllers.Remove(enemy);
		if(!playersTurn && finishedActors >= enemyControllers.Count)
			BeginPlayerTurn();
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
	}

	void EnemyTurnFinished() {
		finishedActors++;
		if(finishedActors >= enemyControllers.Count) {
			TurnEndedEvent();
			BeginPlayerTurn();
		}
	}

}