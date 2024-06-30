using UnityEngine;

public class SleepingState : State
{
    private int animationLayerIndex;
    public Animator animator;
    
    
    private void Awake(){
        animationLayerIndex = animator.GetLayerIndex("Sleeping");
    }
    public override State OnEnterState() {
        animator.SetLayerWeight(animationLayerIndex, 1);
        return this;
    }

    public override void OnExitState() {
        animator.SetLayerWeight(animationLayerIndex, 0);
    }
    
}
