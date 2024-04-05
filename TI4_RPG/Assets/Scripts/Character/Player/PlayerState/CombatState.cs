using UnityEngine;

public class CombatState : PState
{
    [Space(10)]
    [Header("State Properties")]
    public Character agent;
    public Character target;
    
    [Space(5)]
    [Header("Movement Properties")]
    public IMovement movement;
    public CharacterController cc;
    public Vector2 moveDir;
    
    [Space(5)]
    [Header("Animation Atributes")]
    public AnimationController animationController;
    public Animator animator;
    public RuntimeAnimatorController ac;
    private RotationBehaviour targetLock, rotator;


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
        Debug.Log("Entered Combat State");
        animator.runtimeAnimatorController = ac;
        return this;
    }

    public override void OnExitState()
    {
        Debug.Log("Exiting Combat State");
    }
    
}
