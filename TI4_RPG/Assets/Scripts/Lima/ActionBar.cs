using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    [SerializeField] private Image barFill;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color highlight = new Color(225, 200,45, 255 );
    [SerializeField] private Color darkened = new Color(95, 85, 20, 255 );
    [SerializeField] private float sizeChangeDuration = 1;
    [SerializeField] private float sizeChangeModifier = 2;
    private bool full;
    private RectTransform rect;
    
    [SerializeField] private AudioSource audioSource;

    private void Start() {
        barFill.color = darkened;
        rect = GetComponent<RectTransform>();
    }
    
    public void UpdateBar(MyStat stat) {
        text.text = $"{stat.currentValue}/{stat.maxValue}";
        barFill.fillAmount = (float)stat.currentValue / stat.maxValue;
        if (barFill.fillAmount >= 1 && !full) {
            HighLight();
            full = true;
        }
        else if (full && barFill.fillAmount < 1) {
            Unhighlight();
            full = false;
        }
    }

    private void HighLight() {
        barFill.color = highlight;
	    audioSource.Play();
        StartCoroutine(ChangeSize(sizeChangeModifier));
    }

    private void Unhighlight() {
        barFill.color = darkened;
        float size = 1f / sizeChangeModifier;
        StartCoroutine(ChangeSize(size));
    }

    IEnumerator ChangeSize(float mod) {
        Debug.Log("modifier :" + mod);
        float interpolator = 0;
        Vector3 oSize = rect.localScale;
        
        while (interpolator < 1) {
            rect.localScale = Vector3.Lerp(oSize, oSize * mod, interpolator);
            interpolator += Time.unscaledDeltaTime / sizeChangeDuration;
            yield return null;
        }
        rect.localScale = oSize * mod;
    }
}
