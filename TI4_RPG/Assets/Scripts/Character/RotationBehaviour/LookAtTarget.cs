using UnityEngine;

public class LookAtTarget : RotationBehaviour
{
    public LookAtTarget(Transform agent) : base(agent){}

    public override void SetRotation(Vector3 targetPos) => agent.LookAt(targetPos);
}
