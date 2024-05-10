using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject cam;
    public GameObject player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam.transform.position = new Vector3(30, 14, -40);
            player.transform.position = new Vector3(30, 1, -35);
        }
    }
}
