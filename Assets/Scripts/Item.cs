public class Item
{
    [Inject] public GlobalTextArea textArea { private get; set; }
    public ItemEffect effect { private get; set; }
    public string name { private get; set; }
    public float jamChance = 0.2f;
    public event System.Action jamChecksChanged = delegate { };
    public event System.Action itemJammedEvent = delegate { };
    
	private int baseJamSaves = 0;
	private int jamSavesUsed = 0;
	private bool isJammed = false;
    private int numItems = 0;
    private Character character;

    public string GetName()
    {
        return name;
    }

    public int GetNumItems()
    {
        return numItems;
    }

    public void SetNumItems(int newNum)
    {
        var numWas = numItems;
        numItems = newNum;

        effect.NumItemsChanged(numWas, newNum);
    }

    public bool CanActivate()
    {
        return effect.CanActivate();
    }

    public void Activate()
    {
        effect.Activate();
    }

    public void JamCheck()
    {
		if (!IsJammed() && DidFailJamChance())
			AttemptToJamItem();
    }

	bool DidFailJamChance() 
	{
		return UnityEngine.Random.value < jamChance;
	}

    public bool IsJammed()
    {
        return isJammed;
    }

	void AttemptToJamItem() 
	{
		if(HasRemainingJamSaves()) 
			UseAJamSave();
		else
			JamItem();
    }

	bool HasRemainingJamSaves() 
	{
		return jamSavesUsed < baseJamSaves;
	}

	void UseAJamSave() 
	{
		jamSavesUsed++;
        jamChecksChanged();
	}

	void JamItem() 
	{
        isJammed = true;
        itemJammedEvent();
    }

    public void FixJam()
    {
        isJammed = false;
		jamSavesUsed = 0;
        textArea.AddLine(name + " has been fixed.");
        jamChecksChanged();
    }

	public void SetBaseJamSaves(int saves) 
	{
		baseJamSaves = saves;
	}

    public int GetRemainingJamSaves()
    {
        return baseJamSaves - jamSavesUsed;
    }

    public void UnEquip(Character character)
    {
        effect.UnEquip(character);
    }

    public void Equip(Character character)
    {
        effect.Equip(character);
    }

    public int GetTotalJamSaves()
    {
        return baseJamSaves;
    }
}