using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Exploring : State
{
    [Space(10)]
    [Header("State Properties")]
    public Character agent;
    public UnityEvent interact;
    public SkillContainer skillManager;
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
    //public PlayerInput playerInput;
    

    private void Awake(){
        movement = new CCMovement(cc);
        animationController = new DefaultController(animator);
        animationLayerIndex = animator.GetLayerIndex("Exploration");
        rotator = new LookAtMoveDir(transform);
        enabled = false;
        interact.RemoveAllListeners();
    }

    private void OnEnable() {
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").started += OnMovement;
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").performed += OnMovement;
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").canceled += OnMovement;
        InputManager.Instance.actions["Interact"].performed += Interact;
    }

    private void OnDisable() {
        if(InputManager.Instance == null) return;
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").started -= OnMovement;
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").performed -= OnMovement;
        InputManager.Instance.actions.FindActionMap("Action").FindAction("Movement").canceled -= OnMovement;
        InputManager.Instance.actions["Interact"].performed -= Interact;
        moveDir = Vector3.zero;
    }
    
    /*
    public void OnSubscribe() {
        InputManager.Instance.actions["Movement"].started += OnMovement;
        InputManager.Instance.actions["Movement"].performed += OnMovement;
        InputManager.Instance.actions["Movement"].canceled += OnMovement;
        InputManager.Instance.actions["Interact"].performed += Interact;
    }
    
    public void OnCleanup() {
        InputManager.Instance.actions["Movement"].started -= OnMovement;
        InputManager.Instance.actions["Movement"].performed -= OnMovement;
        InputManager.Instance.actions["Movement"].canceled -= OnMovement;
        InputManager.Instance.actions["Interact"].performed -= Interact;
    }
    */

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
        //if(GameManager.Instance.playerInput == null) GameManager.Instance.playerInput = FindObjectOfType<PlayerInput>();
        InputManager.Instance.SwitchCurrentActionMap("Action");
        InputManager.Instance.actions["Interact"].performed += Interact;
        animator.SetLayerWeight(animationLayerIndex, 1);
        animator.runtimeAnimatorController = ac;
        return this;
    }

    public override void OnExitState() {
        animator.SetLayerWeight(animationLayerIndex, 0);
        InputManager.Instance.actions["Interact"].performed -= Interact;
        interact.RemoveAllListeners();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if(!agent.actionable)return;
        if (context.performed) {
            interact?.Invoke();
        }
    }
    public void OutCombtCast(Skill skill)
    {
        skillManager.Cast(skill);
    }
    public void OutCombtCast(int slot)
    {
        skillManager.Cast(slot);
    }
}
