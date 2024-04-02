using UnityEngine;

public abstract class PState : MonoBehaviour
{
    public abstract PState OnEnterState();
    public abstract void OnExitState();
}
