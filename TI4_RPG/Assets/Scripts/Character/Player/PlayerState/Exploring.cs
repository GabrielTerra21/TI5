using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Exploring : PState
{
    [Header("StateProperties")]
    public Character agent;
    public IMovement movement;
    public AnimationController animationController;
    public RotationBehaviour rotator;
    public CharacterController cc;
    public Animator animator;
    public Vector2 moveDir;
    

    private void Awake(){
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        enabled = false;
        rotator = new LookAtMoveDir(transform);
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
        Debug.Log("Entered exploration state");   
        return this;
    }

    public override void OnExitState() {
        Debug.Log("Exiting ExploringState");
    }
}
