using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Exploring : State
{
    [Space(10)]
    [Header("State Properties")]
    public Character agent;
    public Action interact;
    //public List<WaitingTrigger> waitingTriggers;
    //private WaitingTrigger near;
    

    [Space(5)]
    [Header("Movement Properties")]
    public IMovement movement;
    public CharacterController cc;
    public Vector2 moveDir;
    
    [Space(5)]
    [Header("Animation Properties")]
    public AnimationController animationController;
    [SerializeField] private int animationLayerIndex;
    public Animator animator;
    public RuntimeAnimatorController ac;
    public RotationBehaviour rotator;
    

    private void Awake(){
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        animationLayerIndex = animator.GetLayerIndex("Exploration");
        rotator = new LookAtMoveDir(transform);
        enabled = false;
        interact = null;
    }

    private void Update()
    {
        if(paused) return;
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        rotator.SetRotation(moveDir);
    }

    // public void OnMovement(InputValue value) => moveDir = value.Get<Vector2>();
    public void OnMovement(InputAction.CallbackContext context) => moveDir = context.ReadValue<Vector2>();

    public override State OnEnterState() {
        GameManager.Instance.state = GameManager.GameState.EXPLORATION;
        animator.SetLayerWeight(animationLayerIndex, 1);
        animator.runtimeAnimatorController = ac;
        return this;
    }

    public override void OnExitState() {
        animator.SetLayerWeight(animationLayerIndex, 0);
        interact = null;
    }
    public void Interact()
    {
        // foreach(var trigger in waitingTriggers)
        // {
        //     if (near == null || (transform.position - trigger.transform.position).magnitude < (transform.position - near.transform.position).magnitude)
        //     {
        //         near = trigger;
        //     }
        //     trigger.Activate();
        // }
        interact?.Invoke();
    }
}
