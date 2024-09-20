using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void OnEnable() {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
