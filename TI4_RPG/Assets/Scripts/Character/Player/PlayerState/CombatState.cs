using UnityEngine;
using UnityEngine.InputSystem;

public class CombatState : State
{
    [Space(10)]
    [Header("State Properties")]
    [SerializeField] private Character agent;
    [SerializeField] private Character target;
    [SerializeField] private EngageSphere eDetect;
    [SerializeField] private SkillContainer skillManager;
    
    [Space(5)]
    [Header("Movement Properties")]
    private IMovement movement;
    [SerializeField] private CharacterController cc;
    [SerializeField] private Vector2 moveDir;
    
    [Space(5)]
    [Header("Animation Atributes")]
    public RuntimeAnimatorController ac;
    [SerializeField] private Animator animator;
    private AnimationController animationController;
    private RotationBehaviour targetLock;


    private void Awake()
    {
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        targetLock = new LookAtTarget(transform);
        enabled = false;
    }

    private void Update()
    {
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        targetLock.SetRotation(target.transform.position);
        if(moveDir.magnitude < 0.05f) skillManager.AutoAttack(target);
    }

    private void OnMovement(InputValue value) => moveDir = value.Get<Vector2>();

    public void Cast(int slot) {
        skillManager.Cast(slot, target);
    }
    
    public override State OnEnterState()
    {
        Debug.Log("Entered Combat State");
        animator.runtimeAnimatorController = ac;
        target = eDetect.GetNextTarget();
        return this;
    }

    public override void OnExitState() {
        target = null;
        Debug.Log("Exiting Combat State");
    }
    
}
