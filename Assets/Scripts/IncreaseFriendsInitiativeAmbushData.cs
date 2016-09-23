public class IncreaseFriendsInitiativeAmbushData : AmbushActivatorData
{
    public int initiativeAmount = 8;
    public IncreaseFriendsInitiativeAmbush.TurnsToIncrease turnsToIncrease;

    public override AmbushActivator Create()
    {
        var ambush = DesertContext.StrangeNew<IncreaseFriendsInitiativeAmbush>();
        ambush.initiativeGain = initiativeAmount;
        ambush.toIncrease = turnsToIncrease;
        return ambush;
    }
}