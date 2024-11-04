using System.Collections;
using UnityEngine;

public abstract class LimaUILib : MonoBehaviour {
    private RectTransform rect;
    protected void Start() {
        try {
            rect = GetComponent<RectTransform>();
        }
        catch {
            rect = GetComponentInChildren<RectTransform>();
        }
    }
    
    public void SmoothTranslateHorizontal(float distance) {
        Vector3 OPos = rect.position;
        Vector3 FPos = OPos + Vector3.right * distance;

        rect.position = FPos;
    }

    IEnumerator SmoothChangeSize(float amount) {
        Vector3 oSize = rect.localScale;
        Vector3 fSize = oSize + Vector3.one * amount;

        float interpolator = 0;

        while(interpolator <= 1){
            rect.localScale = Vector3.Lerp(oSize, fSize, interpolator);
            interpolator += Time.unscaledDeltaTime;
            yield return null;
        }
        rect.localScale = fSize;
    }
}
