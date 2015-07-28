using UnityEngine;

public class RandomNameGenerator {
	public string[] namePrefixes  = new string[]{
		"Ke", "Hun", "Jur", "Sha", "Gua", "Fet", "Del", "Mal", "Et", "Esh", "Wer", "Vel"
	};

	public string[] namePostfixes  = new string[]{
		"line", "bok", "tes", "sie", "ruk", "rul", "bolan", "aj", "yew", "ad", "weq"
	};

	public string[] townPrefixes = new string[] {
		"North ", "West ", "East ", "South "
	};

	public string[] townPostfixes = new string[]{
		"'s Rest", "'s Passage", "'s Pit", " Stable", "hunts", " Town"
	};

	public string[] cityPrefixes = new string[] {
		"New "
	};

	public string[] cityPostfixes = new string[]{
		"rush", " City", " Center", 
	};

	public string GetCityName() {
		var val = Random.value;
		if(val > 0.9f)
			return cityPrefixes[Random.Range(0, cityPrefixes.Length)] + GetHumanName();
		else if(val > 0.4f)
			return GetHumanName() + cityPostfixes[Random.Range(0, cityPostfixes.Length)];
		else if(val > 0.35f)
			return cityPrefixes[Random.Range(0, cityPrefixes.Length)] + GetHumanName() + cityPostfixes[Random.Range(0, cityPostfixes.Length)];
		else if(val > 0.2f)
			return GetHumanName();
		else
			return namePrefixes[Random.Range(0, namePrefixes.Length)];
	}	

	public string GetTownName() {
		var val = Random.value;
		if(val > 0.65f)
			return townPrefixes[Random.Range(0, townPrefixes.Length)] + GetHumanName();
		else if(val > 0.3)
			return GetHumanName() + townPostfixes[Random.Range(0, townPostfixes.Length)];
		else
			return GetHumanName();
	}

	public string GetHumanName() {
		return namePrefixes[Random.Range(0, namePrefixes.Length)] + namePostfixes[Random.Range(0, namePostfixes.Length)];
	}
}