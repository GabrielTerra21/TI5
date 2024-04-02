using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Exploring : PState
{
    [Header("StateProperties")]
    public Character agent;
    public IMovement movement;
    public AnimationController animationController;
    public CharacterController cc;
    public Animator animator;
    public Vector2 moveDir;
    

    private void Awake(){
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        enabled = false;
    }

    private void Update()
    {
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        LookAtMoveDir();
    }
    
    private void LookAtMoveDir() //Set player's rotation to look towards last/current movement input direction
    {
        transform.LookAt(agent.transform.position + new Vector3(moveDir.x, 0, moveDir.y));
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
