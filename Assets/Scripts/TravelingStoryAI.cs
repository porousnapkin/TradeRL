using UnityEngine;

public class TravelingStoryAI {
	[Inject] public MapPlayerController mapPlayerController { private get; set; }
	public TravelingStoryAIRoutine activeRoutine { private get; set; }
	public int closeTriggerDistance { private get; set; }
	public TravelingStoryAIRoutine closeAI { private get; set; } 
	public int farTriggerDistance { private get; set; }
	public TravelingStoryAIRoutine farAI { private get; set; }
	public event System.Action runningCloseAI = delegate{};
	public event System.Action runningFarAI = delegate{};

	public bool DoesAct() {
		return activeRoutine.DoesAct();
	}

	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		return activeRoutine.GetMoveToPosition(currentPosition);
	}

	//TODO: Need visualization for state changes...
	public void FinishedMove(Vector2 currentPosition) {
		var dist = Mathf.RoundToInt(Vector2.Distance(mapPlayerController.position, currentPosition));

		if(dist <= closeTriggerDistance && closeAI != null) {
			activeRoutine = closeAI;
			runningCloseAI();
		}
		else if(dist >= farTriggerDistance && farAI != null) {
			activeRoutine = farAI;
			runningFarAI();
		}
	}
}