using System.Collections.Generic;
using System;

public class CombatAI : CombatActor{
    List<CombatAINode> nodes = new List<CombatAINode>();
    public AIAbility fallbackAbility;

    public void AddNode(CombatAINode node)
    {
        nodes.Add(node);
    }

    public void Act(System.Action callback)
    {
        foreach(var node in nodes)
        {
            if(node.CanUse())
            {
                node.Perform(() => callback());
                return;
            }
        }

        fallbackAbility.PerformAction(() => callback());
    }

    public void Cleanup()
    {
    }
}
