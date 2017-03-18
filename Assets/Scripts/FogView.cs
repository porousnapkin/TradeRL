using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogView : MonoBehaviour {
    public Material normalFogMat;
    public Material dimmedFogMat;
    public SpriteRenderer distortedAlphaSR;
    public SpriteRenderer whiteSR;

	void Start () {
        distortedAlphaSR.material = normalFogMat;
	}
	
    public void Dim()
    {
        distortedAlphaSR.enabled = true;
        whiteSR.enabled = false;
        distortedAlphaSR.material = dimmedFogMat;
    }

    public void Undim()
    {
        whiteSR.enabled = false;
        distortedAlphaSR.enabled = false;
    }
}
