using UnityEngine;

public class AttackAbilityDrawer : MonoBehaviour
{
    public int minDamage = 5;
    public int maxDamage = 20;

    void Start()
    {
        var popup = GetComponent<UIImageRaycasterPopup>();
        var popupSpace = popup.ReserveSpace();

        if(minDamage != maxDamage)
            popup.Record("" + minDamage + "-" + maxDamage + " damage", popupSpace);
        else
            popup.Record("" + minDamage + " damage", popupSpace);
    }
}

