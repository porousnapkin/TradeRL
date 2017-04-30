public class AIShieldGeneratorEffectData : AISpecialEffectData {
    public int shieldGeneratedPerTurn = 4;
    public int maxShieldGeneratable = 4;

    public override void Apply(Character character)
    {
        var effect = new ShieldGeneratorEffect();
        effect.Apply(character);
    }
}
