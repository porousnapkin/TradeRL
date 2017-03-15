using UnityEngine; 
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl; 
using System;

public class DooberFactoryView : DesertView {
	public GameObject damageDooberPrefab;	
	public GameObject missPrefab;
	public GameObject healPrefab;
	public GameObject abilityMessagePrefab;
	public float healthDooberOffset = 0.5f;

	public void CreateDamageDoober(Vector3 referencePosition, int damage) {
		var dooberGO = CreateHealthDoober(referencePosition, damageDooberPrefab);
		dooberGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-" + damage;
	}

	GameObject CreateHealthDoober(Vector3 referencePosition, GameObject dooberPrefab) {
		var dooberGO = GameObject.Instantiate(dooberPrefab) as GameObject;
		dooberGO.transform.position = referencePosition + new Vector3(0, healthDooberOffset, 0);
		return dooberGO;
	}

	public void CreateMissDoober(Vector3 referencePosition) {
		CreateHealthDoober(referencePosition, missPrefab);
	}

	public void CreateHealDoober(Vector3 referencePosition, int amount) {
		var dooberGO = CreateHealthDoober(referencePosition, healPrefab);
		dooberGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "+" + amount;
	}

	public void CreateAbilityMessagePrefab(Vector3 referencePosition, string message) {
		var dooberGO = CreateHealthDoober(referencePosition, abilityMessagePrefab);
		dooberGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
	}
}

public class DooberFactoryMediator : Mediator {
	[Inject] public DooberFactoryView view { private get; set; }
	[Inject] public DooberFactory dooberFactory { private get; set; }

	public override void OnRegister() 
	{
		dooberFactory.createDamageDooberEvent += view.CreateDamageDoober;
		dooberFactory.createMissDooberEvent += view.CreateMissDoober;
		dooberFactory.createHealDooberEvent += view.CreateHealDoober;
		dooberFactory.createAbilityMessageDooberEvent += view.CreateAbilityMessagePrefab;
	}

	public override void OnRemove ()
	{
		dooberFactory.createDamageDooberEvent -= view.CreateDamageDoober;
		dooberFactory.createMissDooberEvent -= view.CreateMissDoober;
		dooberFactory.createHealDooberEvent -= view.CreateHealDoober;
		dooberFactory.createAbilityMessageDooberEvent -= view.CreateAbilityMessagePrefab;
	}
}

public class DooberFactory {
	public event Action<Vector3, int> createDamageDooberEvent = delegate{};
	public event Action<Vector3> createMissDooberEvent = delegate{};
	public event Action<Vector3, int> createHealDooberEvent = delegate{};
	public event Action<Vector3, string> createAbilityMessageDooberEvent = delegate{};
	
	public void CreateDamageDoober(Vector3 pos, int damage) {
		createDamageDooberEvent(pos, damage);
	}
	
	public void CreateMissDoober(Vector3 pos) {
		createMissDooberEvent(pos);
	}
	
	public void CreateHealDoober(Vector3 pos, int healAmount) {
		createHealDooberEvent(pos, healAmount);
	}
	
	public void CreateAbilityMessageDoober(Vector3 pos, string text) {
		createAbilityMessageDooberEvent(pos, text);
	}
}
