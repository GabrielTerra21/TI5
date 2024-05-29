using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRise : MonoBehaviour
{
    public GameObject logBefore;
    public GameObject logAfter;
    List<GameObject> controller = new List<GameObject>();
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        controller.Add(other.gameObject);
        logBefore.SetActive(false);
        logAfter.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        controller.Remove(other.gameObject);
        if(controller.Count == 0){
            logAfter.SetActive(false);
            logBefore.SetActive(true);
        }
    }


}
