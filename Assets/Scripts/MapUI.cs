using UnityEngine;
using System.Collections;

public class MapUI : MonoBehaviour {
    void Start () {
        gameObject.SetActive(true);
        GlobalEvents.CombatStarted += () => gameObject.SetActive(false);
        GlobalEvents.CombatEnded += () => gameObject.SetActive(true);
	}
}
