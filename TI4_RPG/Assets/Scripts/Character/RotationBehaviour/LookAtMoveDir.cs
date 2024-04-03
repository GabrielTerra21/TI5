using UnityEngine;

public class LookAtMoveDir : RotationBehaviour
{
    public LookAtMoveDir(Transform agent) : base(agent){}
    
    public override void SetRotation(Vector3 moveDir) => agent.LookAt(agent.transform.position + new Vector3(moveDir.x, 0, moveDir.y));
}
