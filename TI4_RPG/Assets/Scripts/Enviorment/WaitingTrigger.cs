using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTrigger : Trigger
{
    private bool waiting = false;
    protected void OnTriggerEnter()
    {
        waiting = true;
    }
    private void OnTriggerExit(Collider other)
    {
        waiting = false;
    }
    private void Update()
    {
        if (waiting && Input.GetKeyDown(KeyCode.E))
        {
            action.Invoke();
            Debug.Log("apertou");
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .2f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.extents * 2);
    }
}
