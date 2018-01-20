using System.Collections.Generic;
using System;

public class CombatAI : CombatActor{
    List<CombatAINode> nodes = new List<CombatAINode>();
    CombatAINode nodeToUse;
    public AIAbility fallbackAbility;
    public CombatController controller;

    public void AddNode(CombatAINode node)
    {
        nodes.Add(node);
    }

    public void SetupAction(Action callback)
    {
        nodeToUse = null;
        foreach (var node in nodes)
        {
            if (node.CanUse())
            {
                nodeToUse = node;
                nodeToUse.SetupAction((ability) =>
                {
                    controller.character.broadcastPreparedAIAbility(ability);
                    callback();
                });
                return;
            }
        }
    }

    public void Act(System.Action callback)
    {
        if (nodeToUse != null)
            nodeToUse.Perform(callback);
        else
            fallbackAbility.PerformAction(() => callback());
    }

    public void Cleanup()
    {
    }
}
