using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State OnEnterState();
    public abstract void OnExitState();
}
