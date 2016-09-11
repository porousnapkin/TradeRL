using UnityEngine;

public interface Restriction {
    bool CanUse();

    void SetupVisualization(GameObject go);
}