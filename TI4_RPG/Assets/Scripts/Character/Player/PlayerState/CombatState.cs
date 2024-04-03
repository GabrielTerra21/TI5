using UnityEngine;

public class CombatState : PState
{
    [Header("StateProperties")]
    public Character agent;
    public Character target;
    public IMovement movement;
    public AnimationController animationController;
    private RotationBehaviour targetLock, rotator;
    public CharacterController cc;
    public Animator animator;
    public Vector2 moveDir;


    private void Awake()
    {
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        targetLock = new LookAtTarget(transform);
        rotator = new LookAtMoveDir(transform);
        enabled = false;
    }

    private void Update()
    {
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        if (target != null) targetLock.SetRotation(target.transform.position);
        else rotator.SetRotation(moveDir);
    }
    
    public override PState OnEnterState()
    {
        return this;
    }

    public override void OnExitState()
    {
        
    }
    
}
