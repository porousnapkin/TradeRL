using System;

public class Item
{
    [Inject] public GlobalTextArea textArea { private get; set; }
    public ItemEffect effect { private get; set; }
    public string name { private get; set; }
    public float jamChance = 0.2f;
    
	private int baseJamSaves = 0;
	private int jamSavesUsed = 0;
	private bool isJammed = false;
    private int numItems = 0;

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
		return jamSavesUsed > baseJamSaves;
	}

	void UseAJamSave() 
	{
		jamSavesUsed++;
		textArea.AddLine(name + " clicks uncomfortably...");
	}

	void JamItem() 
	{
		isJammed = UnityEngine.Random.value < jamChance;
		jamSavesUsed = 0;
		textArea.AddLine(name + " jammed!");
	}

    public void FixJam()
    {
        isJammed = false;
		jamSavesUsed = 0;
        textArea.AddLine(name + " has been fixed.");
    }

	public void SetBaseJamSaves(int saves) 
	{
		baseJamSaves = saves;
	}
}