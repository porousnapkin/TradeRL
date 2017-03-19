using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeightedRandomPicker  {
    class WeightAndIndex
    {
        public int index;
        public int weight;
    }


    public static int PickRandomWeightedIndex(List<int> indexedWeights)
    {
        if (indexedWeights.Count == 0)
            return 0;

        var weightsAndIndices = new List<WeightAndIndex>();
        for(int i = 0; i < indexedWeights.Count; i++) {
            var entry = new WeightAndIndex();
            entry.index = i;
            entry.weight = indexedWeights[i];
            weightsAndIndices.Add(entry);
        }

        var total = indexedWeights.Sum();
        weightsAndIndices = (from element in weightsAndIndices
                      orderby element.weight descending
                      select element).ToList();

        var pickWeight = Random.Range(1, total+1);
        for(int i = 0; i < weightsAndIndices.Count; i++)
        {
            if (weightsAndIndices[i].weight >= pickWeight)
                return weightsAndIndices[i].index;
            else
                pickWeight -= weightsAndIndices[i].weight;
        }

        return weightsAndIndices[weightsAndIndices.Count-1].index;
    }
}
