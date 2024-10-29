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
}
