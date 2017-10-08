using UnityEngine;

public class FogView : MonoBehaviour {
    public Material normalFogMat;
    public Material dimmedFogMat;
    public SpriteRenderer distortedAlphaSR;
    bool hasUndimmed = false;

	void Start () {
        distortedAlphaSR.material = normalFogMat;
	}

    public void Reset()
    {
        distortedAlphaSR.material = normalFogMat;
    }
	
    public void Dim()
    {
        if (!hasUndimmed)
            return;

        distortedAlphaSR.enabled = true;
        distortedAlphaSR.material = dimmedFogMat;
    }

    public void Undim()
    {
        hasUndimmed = true;
        distortedAlphaSR.enabled = false;
    }
}
