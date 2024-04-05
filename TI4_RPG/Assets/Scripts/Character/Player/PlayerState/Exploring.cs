using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Exploring : PState
{
    [Space(10)]
    [Header("State Properties")]
    public Character agent;
    
    [Space(5)]
    [Header("Movement Properties")]
    public IMovement movement;
    public CharacterController cc;
    public Vector2 moveDir;
    
    [Space(5)]
    [Header("Animation Properties")]
    public AnimationController animationController;
    public Animator animator;
    public RuntimeAnimatorController ac;
    public RotationBehaviour rotator;
    

    private void Awake(){
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        rotator = new LookAtMoveDir(transform);
        enabled = false;
    }

    private void Update()
    {
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        rotator.SetRotation(moveDir);
    }

    private void OnMovement(InputValue value) => moveDir = value.Get<Vector2>();

    public override PState OnEnterState()
    {
        Debug.Log("Entered Exploring state");
        animator.runtimeAnimatorController = ac;
        return this;
    }

    public override void OnExitState() {
        Debug.Log("Exiting Exploring State");
    }
}
