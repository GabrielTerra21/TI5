using UnityEngine;

public class DefaultController : AnimationController
{

    public DefaultController(Animator animator) : base(animator){}

    public override void SetAnimations(Vector3 moveDir){
        //Set Walking animation according to player's current movement speed
        WalkAnimation(moveDir);
    }

    private void WalkAnimation(Vector2 moveDir){
        animator.SetFloat("Movement", moveDir.magnitude);
    }

}
