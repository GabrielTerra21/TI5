using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class StayingTrigger : Trigger{

    public UnityEvent secondaryEvent;
    [SerializeField]public List<Collider> colliders;

    private void OnTriggerExit(Collider other) { colliders.Remove(other); Check(); }

    private void OnTriggerEnter(Collider other)
    {
        AddCollider(other);
        action.Invoke();
    }
    public void AddCollider(Collider other) { colliders.Add(other); }
    
    public void Check()
    {
        if(colliders.Count == 0)
        {
            secondaryEvent.Invoke();
        }
    }
}
