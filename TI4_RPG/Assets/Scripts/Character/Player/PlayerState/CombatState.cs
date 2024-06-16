using UnityEngine;
using UnityEngine.InputSystem;

public class CombatState : State {
    [Space(10)] [Header("State Components")] 
    [SerializeField] private SplineLine line;
    [SerializeField] private Character agent;
    [SerializeField] private Character target;
    [SerializeField] private EngageSphere eDetect;
    public SkillContainer skillManager;
    public GameObject skillWheel;
    
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
    [SerializeField] private int animationLayerIndex;
    private RotationBehaviour targetLock;


    protected override void Awake()
    {
        base.Awake();
        movement = new CCMovement(cc);
        animationController = new CombatController(animator);
        animationLayerIndex = animator.GetLayerIndex("Combat");
        targetLock = new LookAtTarget(transform);
        enabled = false;
        if (!line) line = GetComponentInChildren<SplineLine>();
    }

    private void Update()
    {
        if(paused) return;
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        targetLock.SetRotation(target.transform.position);
        if(moveDir.magnitude < 0.05f) skillManager.AutoAttack(target);
    }

    // private void OnMovement(InputValue value) => moveDir = value.Get<Vector2>();
    public void OnMovement(InputAction.CallbackContext context) => moveDir = context.ReadValue<Vector2>();

    public void Cast(int slot) {
        skillManager.Cast(slot, target);
    }

    public void Cast(Skill skill) {
        skillManager.Cast(skill, target);
    }
    
    public override State OnEnterState()
    {
        Debug.Log("Entered Combat State");
        // skillWheel.SetActive(true);
        animator.SetLayerWeight(animationLayerIndex, 1);
        animator.runtimeAnimatorController = ac;
        target = eDetect.GetNextTarget();
        line.gameObject.SetActive(true);
        line.Target(target.LockOnTarget);
        return this;
    }

    public void TargetNext() {
        target = eDetect.GetNextTarget(target);
        Debug.Log($"target is now {target}");
        line.Target(target.LockOnTarget.gameObject);
    }
    
    public override void OnExitState() {
        // skillWheel.SetActive(false);
        target = null;
        animator.SetLayerWeight(animationLayerIndex, 0);
        line.gameObject.SetActive(false);
        Debug.Log("Exiting Combat State");
    }
    
    public Character ReturnTarget()
    {
        return target;
    }
}
