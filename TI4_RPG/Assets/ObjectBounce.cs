using UnityEngine;

public class ObjectBounce : MonoBehaviour {
    [SerializeField] private float range = 1;
    [SerializeField] private float speed = 1;

    private void FixedUpdate() {
        float height = Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * 0.5f + 0.5f * range;
        transform.position += Vector3.up * height;
    }
}
