using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetHighlighter {
    List<GameObject> activeHighlights = new List<GameObject>();
    bool isHighlighting = false;

    public void HighlightTargets(List<Character> targets)
    {
        if (isHighlighting)
            RemoveAllHighlights();

        var prefab = CombatReferences.Get().highlightPrefab;
        targets.ForEach(c =>
        {
            var referenceTransform = c.ownerGO.transform;
            var highlight = GameObject.Instantiate(prefab, referenceTransform.position, Quaternion.identity) as GameObject;
            activeHighlights.Add(highlight);
        });

        isHighlighting = true;
    }

    public void RemoveAllHighlights()
    {
        activeHighlights.ForEach(h =>
            GameObject.Destroy(h)
        );
        activeHighlights.Clear();

        isHighlighting = false;
    }
}
