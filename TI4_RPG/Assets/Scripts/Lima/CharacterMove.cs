using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public GameObject player;
    public int x;
    public int y;
    public int z;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("É o player");
            player.transform.position = new Vector3(x, y, z);
        }
    }
}
