public class MakeRestImpossibleEffectActionData : EffectActionData
{
    public override EffectAction Create(Character character)
    {
        return DesertContext.StrangeNew<MakeRestImpossibleEffectAction>();
    }
}