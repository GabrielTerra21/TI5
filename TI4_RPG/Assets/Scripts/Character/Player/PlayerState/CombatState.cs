using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CombatState : State {
    [Space(10)] [Header("State Components")] 
    [SerializeField] private SplineLine line;
    public Character agent;
    [SerializeField] private Character target;
    [SerializeField] private EngageSphere eDetect;
    //public SkillContainer skillManager;
    
    [Space(10)][Header("New Combat System Components")]
    [SerializeField] private SkillDataSO autoAttack;
    [SerializeField] private float coolDown;
    public SkillDataSO[] skills = new SkillDataSO[6]; 
    
    
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


    private void Awake()
    {
        movement = new CCMovement(cc);
        animationController = new CombatController(animator);
        animationLayerIndex = animator.GetLayerIndex("Combat");
        targetLock = new LookAtTarget(transform);
        enabled = false;
        if (!line) line = GetComponentInChildren<SplineLine>();
    }

    private void OnEnable() {
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").started += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").performed += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").canceled += OnMovement;
        InputManager.Instance.actions["SwitchEnemy"].performed += TargetNext;
    }

    private void OnDisable() {
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").started += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").performed += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").canceled += OnMovement;
        InputManager.Instance.actions["SwitchEnemy"].performed -= TargetNext;
    }
    
    /*
    //Subscreve as respectivas ações ao mapa de ações do input Manager
    public void OnSubscribe() {
        playerInput.actions["Movement"].started += OnMovement;
        playerInput.actions["Movement"].performed += OnMovement;
        playerInput.actions["Movement"].canceled += OnMovement;
        playerInput.actions["SwitchEnemy"].performed += TargetNext;
    }
    
    //Desubscreve as respectivas ações ao mapa de ações do input Manager
    public void OnCleanup() {
        playerInput.actions["Movement"].started -= OnMovement;
        playerInput.actions["Movement"].performed -= OnMovement;
        playerInput.actions["Movement"].canceled -= OnMovement;
        playerInput.actions["SwitchEnemy"].performed -= TargetNext;
    }*/

    private void Update()
    {
        if(paused) return;
        movement.Moving(moveDir, agent.moveSpeed);
        animationController.SetAnimations(moveDir);
        targetLock.SetRotation(target.transform.position);
        if (moveDir.magnitude < 0.05f && coolDown <= 0) {
            autoAttack.OnCast(agent, target);
            coolDown = autoAttack.CoolDown;
        }
        else if(!paused)coolDown -= Time.deltaTime;
    }

    private void LateUpdate() { OnTargetDeath(); }

    // private void OnMovement(InputValue value) => moveDir = value.Get<Vector2>();
    public void OnMovement(InputAction.CallbackContext context) {
        moveDir = context.ReadValue<Vector2>();
    }

    /*
    public void Cast(int slot) {
        skillManager.Cast(slot, target);
    }

    public void Cast(Skill skill) {
        skillManager.Cast(skill, target);
    }
    */
    
    public override State OnEnterState()
    {
        GameManager.Instance.state = GameManager.GameState.COMBAT;
        InputManager.Instance.SwitchCurrentActionMap("Combat");
        // skillWheel.SetActive(true);
        animator.SetLayerWeight(animationLayerIndex, 1);
        animator.runtimeAnimatorController = ac;
        target = eDetect.GetNextTarget();
        line.gameObject.SetActive(true);
        line.Target(target.LockOnTarget);
        return this;
    }
    
    public override void OnExitState() {
        // skillWheel.SetActive(false);
        target = null;
        animator.SetLayerWeight(animationLayerIndex, 0);
        line.gameObject.SetActive(false);
        Debug.Log("Exiting Combat State");
    }
    
    // Faz chamada do eDetect para adquirir novo alvo
    // Atualiza o alvo da linha de targeting
    public void TargetNext(InputAction.CallbackContext context) {
        target = eDetect.GetNextTarget(target);
        line.Target(target.LockOnTarget.gameObject);
    }

    // Se o jogador realizar o input e possuir barra de ação o suficiente
    // o menu de ataque sera aberto.
    /*public void OpenActionMenu(InputAction.CallbackContext context) {
        if (context.performed && GameManager.Instance.ap.currentValue >= 25) {
            // abre o menu de combate
        }
    }*/
    
    // Mesma coisa que o TargetNext mas não possui argumentos
    // utilizado para chamar o metodo automaticamente caso não haja alvo
    public void TargetNext() {
        if (GameManager.Instance.state != GameManager.GameState.COMBAT) return; // Failsafe
        
        target = eDetect.GetNextTarget(target);
        line.Target(target.LockOnTarget.gameObject);
    }

    // Caso o jogador não possua alvo, automaticamente seleciona um alvo novo
    public void OnTargetDeath() { if (target == null) { TargetNext(); } }
    
    public Character ReturnTarget()
    {
        return target;
    }
}
