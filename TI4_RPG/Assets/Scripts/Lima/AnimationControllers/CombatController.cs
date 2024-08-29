using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : AnimationController
{
    public CombatController(Animator animator) : base(animator){}

    public override void SetAnimations(Vector3 moveDir) {
        animator.SetFloat("MovementX", moveDir.x);
        animator.SetFloat("MovementY", moveDir.y);
    }
}
