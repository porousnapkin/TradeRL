using UnityEngine; 
using UnityEngine.UI; 

public class DooberFactory : MonoBehaviour {
	public GameObject damageDooberPrefab;	
	public GameObject missPrefab;
	public GameObject healPrefab;
	public GameObject abilityMessagePrefab;
	public float healthDooberOffset = 0.5f;

	public void CreateDamageDoober(Vector3 referencePosition, int damage) {
		var dooberGO = CreateHealthDoober(referencePosition, damageDooberPrefab);
		dooberGO.GetComponentInChildren<Text>().text = "-" + damage;
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
		dooberGO.GetComponentInChildren<Text>().text = "+" + amount;
	}

	public void CreateAbilityMessagePrefab(Vector3 referencePosition, string message) {
		var dooberGO = CreateHealthDoober(referencePosition, abilityMessagePrefab);
		dooberGO.GetComponentInChildren<Text>().text = message;
	}
}