using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    public Image barFill;
    public Color highlight = new Color(225, 200,45, 255 );
    public Color darkened = new Color(95, 85, 20, 255 );

    public void UpdateBar(MyStat stat) {
        barFill.fillAmount = (float)stat.currentValue / stat.maxValue;
        if (barFill.fillAmount >= 1f) { HighlightBar(); }
    }

    private void HighlightBar() {
        barFill.color = highlight;
    }
}
