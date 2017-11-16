using UnityEngine;

public class RestEvent : StoryActionEvent
{
    const int restEffortAmount = 2;
    const float percentHealed = 0.25f;
    [Inject] public Effort effort { private get; set; }
    [Inject] public PlayerCharacter player { private get; set; }
    [Inject(StatusEffects.PARTY)] public StatusEffects partyStatus { private get; set; }

    public void Activate(System.Action callback)
    {
        effort.SafeAddEffort(Effort.EffortType.Mental, restEffortAmount);
        effort.SafeAddEffort(Effort.EffortType.Physical, restEffortAmount);
        effort.SafeAddEffort(Effort.EffortType.Social, restEffortAmount);
        var health = player.GetCharacter().health;
        health.Heal(Mathf.RoundToInt(health.MaxValue * percentHealed));

        partyStatus.AddStatusEffect(BasicStatusEffects.Instance.restedEffect.Create(player.GetCharacter()));

        callback();
    }
}