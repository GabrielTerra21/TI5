using UnityEngine;

public abstract class RotationBehaviour
{
    protected Transform agent;

    public RotationBehaviour(Transform agent) => this.agent = agent;
    
    public abstract void SetRotation(Vector3 dir);
}
