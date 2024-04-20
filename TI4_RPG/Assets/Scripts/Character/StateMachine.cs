using UnityEngine;

public abstract class StateMachine : MonoBehaviour {
    [Space(10)]
    [Header("Basic States")]
    [SerializeField] protected State current, start;


    protected virtual void Start() {
        EnterState(start);
    }
    
    public void EnterState(State state)
    {
        current.OnExitState();
        current.enabled = false;
        state.enabled = true;
        current = state.OnEnterState();
    }
}
