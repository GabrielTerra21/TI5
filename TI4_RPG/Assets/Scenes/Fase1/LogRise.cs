using UnityEngine;
using UnityEngine.Events;

public class LogRise : CollisionTrigger{

    public UnityEvent secondaryEvent;
    private void  OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            secondaryEvent.Invoke();
        }
    }
}
