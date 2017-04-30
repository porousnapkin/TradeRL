using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    public Image imageThatFlashes;
    public RectTransform barTransform;
    public RectTransform burnTransform;
    public float lerpTime = 0.5f;
    public bool whiteFlashOnLowered = true;
    public bool whiteFlashOnRaised = true;
    public float whiteFlashTime = 0.1f;
    Color defaultColor;
    float oldPercent = 1.0f;

    void Start()
    {
        defaultColor = imageThatFlashes.color;
    }

    public void SetInitialPercent(float percent)
    {
        oldPercent = percent;
        barTransform.localScale = new Vector3(percent, 1, 1);
    }

    public void SetPercent(float percent) {
        if (percent == oldPercent)
            return;

        LeanTween.cancel(gameObject);

        if (burnTransform == null)
            barTransform.localScale = new Vector3(percent, 1, 1);
        else if (percent < oldPercent)
            SetupLowered(percent);
        else
            SetupRaised(percent);

        oldPercent = percent;
	}

    private void SetupRaised(float percent)
    {
        barTransform.localScale = new Vector3(oldPercent, 1, 1);
        burnTransform.localScale = new Vector3(percent, 1, 1);
        var capturedOld = oldPercent;
        LeanTween.value(gameObject, (p) => barTransform.localScale = new Vector3(p, 1, 1), capturedOld, percent, lerpTime);

        if (whiteFlashOnRaised)
            WhiteFlash();
    }

    private void SetupLowered(float percent)
    {
        burnTransform.localScale = new Vector3(oldPercent, 1, 1);
        barTransform.localScale = new Vector3(percent, 1, 1);
        var capturedOld = oldPercent;
        LeanTween.value(gameObject, (p) => burnTransform.localScale = new Vector3(p, 1, 1), capturedOld, percent, lerpTime);

        if (whiteFlashOnLowered)
            WhiteFlash();
    }

    void WhiteFlash()
    {
        imageThatFlashes.color = Color.white;
        LeanTween.value(gameObject, (c) => imageThatFlashes.color = c, Color.white, defaultColor, whiteFlashTime).setOnComplete(() => imageThatFlashes.color = defaultColor);
    }
}
