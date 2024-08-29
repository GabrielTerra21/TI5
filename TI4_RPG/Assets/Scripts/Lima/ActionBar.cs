using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    [SerializeField] private Image barFill;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color highlight = new Color(225, 200,45, 255 );
    [SerializeField] private Color darkened = new Color(95, 85, 20, 255 );
    private bool full;


    private void Start() {
        barFill.color = darkened;
    }
    
    public void UpdateBar(MyStat stat) {
        text.text = $"{stat.currentValue}/{stat.maxValue}";
        barFill.fillAmount = (float)stat.currentValue / stat.maxValue;
        if (barFill.fillAmount >= 1 && !full) {
            barFill.color = highlight;
            full = true;
        }
        else if (full && barFill.fillAmount < 1) {
            barFill.color = darkened;
            full = false;
        }
    }
}
