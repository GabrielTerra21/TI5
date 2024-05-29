using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform point;
    
    public void Teleportation(GameObject player){
        player.transform.position = point.position;
    }
}
