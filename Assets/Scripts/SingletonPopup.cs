using UnityEngine;
using UnityEngine.UI;

public class SingletonPopup : MonoBehaviour {
    static SingletonPopup singletonPopup;
    public static SingletonPopup Instance { get { return singletonPopup; } }
    public TMPro.TextMeshProUGUI text;
    int asksToShow = 0;
    RectTransform popupTransform;
    public int mouseOffset = 5;

    void Awake()
    {
        singletonPopup = this;
        popupTransform = transform as RectTransform;
    }

    public void ShowPopup(string description)
    {
        text.text = description;
        asksToShow++;
    }

    public void UpdateDescription(string description)
    {
        text.text = description;
    }

    public void DoneWithPopup()
    {
        asksToShow--;
        Mathf.Max(asksToShow, 0);
    }

    void Update()
    {
        transform.SetAsLastSibling();

        if(asksToShow > 0 )
        {
            var rect = popupTransform.rect;
            Vector3 offset = new Vector3(rect.width / 2 + mouseOffset, rect.height / 2 + mouseOffset);
            transform.position = Input.mousePosition + offset;
        }
        else
        {
            transform.position = Vector3.one * 100000;
        }
    }
}
