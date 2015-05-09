using UnityEngine;
using System.Collections.Generic;

public class AbilityButton : MonoBehaviour {
	public string abilityName = "Slam";
	public string abilityDescription = "Hit people";
	public int cooldown = 5;
	public AbilityData abilityData;
	public Ability ability;
	public GridHighlighter gridHighlighter;

	void Start() {
		ability = abilityData.Create(PlayerController.Instance.playerCharacter);
		//Just a test...
		//Invoke("SetupTest", 0.2f);	
	}

	void SetupTest() {
		var cost = new NoCost();
		var inputTargetPicker = new SingleTargetInputPicker();
		inputTargetPicker.inputCollector = GameObject.Find("MapMaker").GetComponent<GridInputCollector>();
		inputTargetPicker.gridHighlighter = gridHighlighter;
		inputTargetPicker.owner = PlayerController.Instance.playerCharacter;
		var targetFilter = new TargetOccupiedInputFilter();
		targetFilter.mapGraph = MapGraph.Instance; //Do this better...
		inputTargetPicker.AddFilter(targetFilter);
		var attackAbilityActivator = new AttackWithDamageMultiplierAbility();
		attackAbilityActivator.ownerCharacter = PlayerController.Instance.playerCharacter;
		attackAbilityActivator.mapGraph = MapGraph.Instance;
		ability = new Ability();
		ability.cost = cost;
		ability.targetPicker = inputTargetPicker;
		ability.activator = attackAbilityActivator;
	}

	public void Activate() {
		ability.Activate();
	}	
}