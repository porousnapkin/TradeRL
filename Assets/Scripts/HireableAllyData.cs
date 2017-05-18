using UnityEngine;

public class HireableAllyData : ScriptableObject {
    public AICharacterData character;
    public string description = "";
    public int initialCost = 100;
    public int costPerTrip = 15;
    public bool getsWounded = true;
}
