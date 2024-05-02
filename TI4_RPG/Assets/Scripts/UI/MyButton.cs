using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyButton : MonoBehaviour {
    public UnityEvent buttonAction;
    public Color BaseColor, HighlightedColor;
    public RectTransform[] rect;
    public Image buttonBackground;
    public TMP_Text buttonText;
    public float sizeMod = 1.25f;


    private void Awake() {
        rect = GetComponentsInChildren<RectTransform>();
        buttonBackground = GetComponentInChildren<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void Do(string menuName) {
        buttonAction.Invoke();
    }

    public void Highlighted() {
        foreach(RectTransform data in rect) data.sizeDelta *= sizeMod;
        buttonBackground.color = HighlightedColor;
        buttonText.color = HighlightedColor;
    }
    
    public void Unhighlighted() {
        foreach(RectTransform data in rect) data.sizeDelta /= sizeMod;
        buttonBackground.color = BaseColor;
        buttonText.color = BaseColor;
    }
}
