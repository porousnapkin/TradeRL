using System;

public class Item
{
    [Inject] public GlobalTextArea textArea { private get; set; }
    public ItemEffect effect { private get; set; }
    public string name { private get; set; }
    public bool canJam = false;
    public float jamChance = 0.2f;
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
        if (!IsJammed())
        {
            isJammed = UnityEngine.Random.value < jamChance;
            textArea.AddLine(name + " jammed!");
        }
    }

    public bool IsJammed()
    {
        return isJammed;
    }

    public void FixJam()
    {
        isJammed = false;
        textArea.AddLine(name + " has been fixed.");
    }
}