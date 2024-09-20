using UnityEngine;

public class keyAnimation : MonoBehaviour {
    public void Deactivate() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.right, Camera.main.transform.forward);
    }
}
