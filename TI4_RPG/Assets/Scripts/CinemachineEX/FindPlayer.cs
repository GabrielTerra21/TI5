using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    private void Awake(){
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        if(cam.Follow == null) cam.Follow = GameObject.FindGameObjectWithTag("CamTarget").transform;
        if(cam.LookAt == null) cam.LookAt = GameObject.FindGameObjectWithTag("CamTarget").transform;
    }
}
