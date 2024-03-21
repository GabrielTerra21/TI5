using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    private void Awake(){
        cam = GetComponent<CinemachineVirtualCamera>();
        if(cam.Follow == null || cam.LookAt == null) Target(GameObject.FindGameObjectWithTag("CamTarget").transform);
    }

    public void Target(Transform target){
        cam.Follow = target;
        cam.LookAt = target;
    }

    public void Stay(){
        if(cam.Follow != null) cam.Follow = null;
        if(cam.LookAt != null) cam.LookAt = null;
    }
}
