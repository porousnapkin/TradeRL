using UnityEngine;

public class CombatReferences : ScriptableObject {
    public static CombatReferences Get()
    {
        return Resources.Load<CombatReferences>("CombatReferences");
    }

    public GameObject highlightPrefab;
    public GameObject combatCharacterPrefab;
    public GameObject combatViewPrefab;
    public PlayerAbilityData emptyAbility;
}
