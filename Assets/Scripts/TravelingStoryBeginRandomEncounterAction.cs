using System;
using System.Linq;
using System.Collections.Generic;

public class TravelingStoryBeginRandomEncounterAction : TravelingStoryAction {
    [Inject] public RandomEncounterGenerator encounterGenerator { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }
    public List<EncounterFaction> encounterFactions { private get; set; }
    public int encounterCost { private get; set; }
    public AIAbilityData ambushAbility { private get; set; }

    public void Activate(System.Action finishedDelegate, bool playerInitiated)
    {
        var group = encounterGenerator.CreateEncounterGroupForFactions(encounterFactions, encounterCost);
        combatFactory.CreateCombat(group, ambushAbility, playerInitiated ? CombatFactory.CombatInitiator.Player : CombatFactory.CombatInitiator.Enemy);
    }
}

