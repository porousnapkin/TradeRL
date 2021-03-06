﻿public class GainEffortEvent : StoryActionEvent
{
    [Inject] public Effort effort { private get; set; }
    public int mental { private get; set; }
    public int social { private get; set; }
    public int physical { private get; set; }

    public void Activate(System.Action callback)
    {
        effort.SafeAddEffort(Effort.EffortType.Mental, mental);
        effort.SafeAddEffort(Effort.EffortType.Social, social);
        effort.SafeAddEffort(Effort.EffortType.Physical, physical);
    }
}
