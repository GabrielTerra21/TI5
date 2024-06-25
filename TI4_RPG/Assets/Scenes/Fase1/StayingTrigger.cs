using UnityEngine;
using UnityEngine.Events;

public class StayingTrigger : CollisionTrigger{

    public UnityEvent secondaryEvent;

    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) secondaryEvent.Invoke(); }
}
