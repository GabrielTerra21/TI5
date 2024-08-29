using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionTrigger : Trigger
{

    protected void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) {
            action.Invoke();
        }
    }

    protected void OnDrawGizmos(){
        Gizmos.color = new Color(0, 1, 0, .2f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.extents * 2);
    }
}
