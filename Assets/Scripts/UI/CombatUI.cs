using UnityEngine;

public class CombatUI : MonoBehaviour {
	void Start () {
        gameObject.SetActive(false);
        GlobalEvents.CombatStarted += () => gameObject.SetActive(true);
        GlobalEvents.CombatEnded += () => gameObject.SetActive(false);
	}
}
