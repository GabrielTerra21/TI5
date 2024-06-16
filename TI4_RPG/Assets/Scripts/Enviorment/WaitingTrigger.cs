using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTrigger : Trigger
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Exploring>().waitingTriggers.Add(this);
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Exploring>().waitingTriggers.Remove(this);
    }
    public void Activate()
    {
        action.Invoke();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .2f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.extents * 2);
    }
}
