using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform entryPos;
    public Door destination;

    public void EnterDoor(){
        Debug.Log("EnteredDoor");
    }
}
