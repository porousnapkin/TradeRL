using UnityEngine;

public class TownTradeBonus : TownTradeModifier
{
    public float bonusPercent = 0.1f;

    public int GetCostOfTradeGoodAdjustment(int baseCost)
    {
        return -Mathf.RoundToInt(baseCost * bonusPercent);
    }

    public int GetPaymentForForeignGoodAdjustment(int baseCost)
    {
        return Mathf.RoundToInt(baseCost * bonusPercent);
    }
}

