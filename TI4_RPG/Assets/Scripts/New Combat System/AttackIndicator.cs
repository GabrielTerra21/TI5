using System.Collections;
using UnityEngine;

public class AttackIndicator : MonoBehaviour {
    [SerializeField] private Color defaultCol;
    [SerializeField] private Color activeCol;
    [SerializeField] private Material mat;
    [SerializeField] private float range;
    [SerializeField] private float fadeRange;
    [SerializeField] private float duration;
    [SerializeField] private LayerMask floorLayer;
    public bool highlighted = false;


    private void Awake() {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Start() {
        transform.localScale = Vector3.zero;
        mat.color = defaultCol;
    }

    private void FixedUpdate() {
        transform.rotation = Quaternion.identity;
    }

    public void SetRange(float range) {
        this.range = range / 5;
    }

    public void Activate() {
        mat.color = activeCol;
        highlighted = true;
    }

    public void Deactivate() {
        mat.color = defaultCol;
        highlighted = false;
    }

    public void FadeIn() {
        StartCoroutine(ChangeSize(range));
    }

    public void FadeOut() {
        StartCoroutine(ChangeSize(fadeRange));
    }

    IEnumerator ChangeSize(float desiredSize) {
        Vector3 OSize = transform.localScale;
        Vector3 finalSize = new Vector3(desiredSize, desiredSize, desiredSize);
        float timer = 0f;
        while (timer < 1) {
            Vector3 value = Vector3.Lerp(OSize, finalSize, timer);
            transform.localScale = value;
            timer += Time.deltaTime/ duration;
            yield return null;
        }
        transform.localScale = finalSize;
    }
    
}
