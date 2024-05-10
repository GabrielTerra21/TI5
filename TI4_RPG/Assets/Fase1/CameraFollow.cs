using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cam;
    public int xCamPre, yCamPre, zCamPre, xCamPos, yCamPos, zCamPos;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(cam.transform.position == new Vector3 (xCamPre, yCamPre, zCamPre))
            {
                cam.transform.position = new Vector3 (xCamPos, yCamPos , zCamPos);
                Debug.Log("Foi para o p�s");
            } else if(cam.transform.position == new Vector3 (xCamPos, yCamPos, zCamPos))
            {
                cam.transform.position = new Vector3 (xCamPre, yCamPre, zCamPre);
                Debug.Log("Foi para a pr�");
            }
        }
        if(cam.transform.position == new Vector3(30, -22, -45))
        {
            player.transform.position = new Vector3(30, -45, -30);
        }
    }
}
