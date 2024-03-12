using UnityEngine;

public class DefaultController : AnimationController
{

    public DefaultController(Player agent) : base(agent){}

    public override void SetAnimations(){
        //Set Walking animation according to player's current movement speed
        WalkAnimation();

        //Set player's rotation to look towards last/current movement input direction
        RotationAnimation();
    }

    private void RotationAnimation(){
        agent.transform.LookAt(agent.transform.position + new Vector3(agent.moveDir.x, 0, agent.moveDir.y));
    }

    private void WalkAnimation(){
        agent.animator.SetFloat("Movement", agent.moveDir.magnitude);
    }

}
