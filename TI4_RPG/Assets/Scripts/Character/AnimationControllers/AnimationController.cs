using UnityEngine;

public abstract class AnimationController
{
    protected Animator animator;


    public AnimationController(Animator animator) => this.animator = animator;

    public abstract void SetAnimations(Vector3 moveDir);
}
