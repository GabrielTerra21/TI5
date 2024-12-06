using UnityEngine;
using UnityEngine.Events;

public class StayingTrigger : Trigger{

    public UnityEvent secondaryEvent;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            secondaryEvent.Invoke();
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            action.Invoke();
        }
    }
}
