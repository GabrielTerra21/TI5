using UnityEngine;

public class bounce : MonoBehaviour {
    private RectTransform rect;
    private Vector3 alteration = new Vector3();
    [SerializeField] private float scale;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        alteration = rect.localPosition;
    }
    
    // Faz o objeto oscilar para cima e para baixo na UI
    private void FixedUpdate() {
        alteration.y = Mathf.Sin(Time.time * Mathf.PI) * 0.5f + 0.5f;
        rect.localPosition = alteration;
    }
}
