using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WorldMapCamera : MonoBehaviour {
    public BaseRaycaster raycaster;
    bool inCombat = false;

	void Start () {
        GlobalEvents.CombatStarted += BeginCombat;
        GlobalEvents.CombatEnded += EndCombat;
	}

    void BeginCombat()
    {
        inCombat = true;
        raycaster.enabled = false;
    }

    void EndCombat()
    {
        inCombat = false;
        raycaster.enabled = true;
    }
	
	void Update () {
        if(!inCombat)
            raycaster.Raycast(new PointerEventData(EventSystem.current), new List<RaycastResult>());
	}
}
