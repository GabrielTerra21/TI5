using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed around each axis
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    void Update()
    {
        // Rotate the GameObject continuously
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
