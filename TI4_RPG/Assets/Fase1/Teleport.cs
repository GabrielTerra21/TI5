using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform point;
    
    public void Teleportation(GameObject player){
        FindObjectOfType(typeof(Exploring)).enable = false;
        player.transform.position = point.position;
        Debug.Log("foi");
    }
}
