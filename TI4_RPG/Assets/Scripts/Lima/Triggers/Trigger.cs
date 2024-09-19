using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    [Space(10)]
    [Header("TriggerProperties")]
    [Tooltip("What does the event trigger")] public UnityEvent action;
    [Tooltip("Determines whether or not the event can be triggered more than once")] public bool oneTime;


    protected virtual void Start() {
        if(oneTime) action.AddListener(() => {
            action = null;
        });
    } 
}
