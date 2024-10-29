using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate() {
        try {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
        catch{}
    }
}
