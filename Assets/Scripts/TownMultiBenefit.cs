using System.Collections.Generic;

public class TownMultiBenefit : TownBenefit
{
    public List<TownBenefit> benefits;

    public void Apply()
    {
        benefits.ForEach(b => b.Apply());
    }

    public void Undo()
    {
        benefits.ForEach(b => b.Undo());
    }
}
