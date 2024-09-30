using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatState : State {
    [Space(10)] [Header("State Components")] 
    [SerializeField] private SplineLine line;
    [SerializeField] private AttackIndicator aoe;
    public Character agent;
    [SerializeField] private Character target;
    [SerializeField] private EngageSphere eDetect;
    //public SkillContainer skillManager;

    [Space(10)]
    [Header("New Combat System Components")]
    //public Action OnEndCombat;
    [SerializeField] private List<Character> enemies = new List<Character>();
    [SerializeField] private SkillDataSO autoAttack;
    [SerializeField] private float attackRange = 1f;
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

    protected override void Start() {
        base.Start();
        aoe.SetRange(attackRange);
    }

    //Subscreve as respectivas ações ao mapa de ações do input Manager
    private void OnEnable() {
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").started += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").performed += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").canceled += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("SwitchEnemy").performed += TargetNext;
    }

    //Desubscreve as respectivas ações ao mapa de ações do input Manager
    private void OnDisable() {
        if (InputManager.Instance == null) return;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").started += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").performed += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").canceled += OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("SwitchEnemy").performed += TargetNext;
        moveDir = Vector3.zero;
    }

    private void Update()
    {
        if(paused) return;
        movement.Moving(moveDir, agent.moveSpeed);
        
        if(target == null) return;
        
        //targetLock.SetRotation(target.transform.position);
        SetRotation();
        if(InRange(target.transform))
        {
            if (moveDir.magnitude < 0.05f && coolDown <= 0) {
                autoAttack.OnCast(agent, target);
                coolDown = autoAttack.CoolDown;
            }
            else if(!paused)coolDown -= Time.deltaTime;
        }
        
    }

    private void SetRotation() {
        if (Vector3.Distance(transform.position, target.transform.position) < autoAttack.Range){
            transform.LookAt(target.transform.position);
            animationController.SetAnimations(moveDir); 
        }
        else {
            transform.LookAt(agent.transform.position + new Vector3(moveDir.x, 0, moveDir.y));
            animator.SetFloat("Movement", moveDir.magnitude);
        }
    }

    private void LateUpdate() { OnTargetDeath(); }
    
    public void OnMovement(InputAction.CallbackContext context) {
        moveDir = context.ReadValue<Vector2>();
    }

    // Calcula 
    private bool InRange(Transform target) {
        if ((target.position - transform.position).sqrMagnitude > attackRange * attackRange) {
            if (aoe.highlighted) aoe.Deactivate();
            return false;
        }
        if (!aoe.highlighted) aoe.Activate();
        return true;
    }
    
    public override State OnEnterState() {
        GameObject[] enemyGroup = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyGroup.Length; i++) {
            enemies.Add(enemyGroup[i].GetComponent<Character>());
        }
        
        //GameManager.Instance.playerStats.FadeIn();
        aoe.FadeIn();
        
        InputManager.Instance.SwitchCurrentActionMap("Combat");
        animator.SetLayerWeight(animationLayerIndex, 1);
        animator.runtimeAnimatorController = ac;
        
        //target = eDetect.GetNextTarget();
        TargetNext();
        line.gameObject.SetActive(true);
        line.Target(target.LockOnTarget);
        
        return this;
    }
    
    // Limpa as variaveis, desliga elementos da interface de combate
    // e altera layer de animação.
    public override void OnExitState() {
        enemies.Clear();
        //GameManager.Instance.playerStats.FadeOut();
        aoe.FadeOut();
        target = null;
        animator.SetLayerWeight(animationLayerIndex, 0);
        line.gameObject.SetActive(false);
        Debug.Log("Exiting Combat State");
    }
    
    // Faz chamada do eDetect para adquirir novo alvo
    // Atualiza o alvo da linha de targeting
    public void TargetNext(InputAction.CallbackContext context) {
        //target = eDetect.GetNextTarget(target);
        Character nTarget;
        int i = enemies.IndexOf(target);
        nTarget = enemies[(i + 1) % enemies.Count];
        target = nTarget;
        line.Target(nTarget.LockOnTarget);
    }
    
    // Mesma coisa que o TargetNext mas não possui argumentos
    // utilizado para chamar o metodo automaticamente caso não haja alvo
    public void TargetNext() {
        //target = eDetect.GetNextTarget(target);
        Character nTarget  = null;
        foreach (var data in enemies) {
            if (data != null) nTarget = data;
        }
        if(nTarget == null){GameManager.Instance.CallExploration();}
        else {
            target = nTarget;
            line.Target(target.LockOnTarget);
        }
    }

    // Caso o jogador não possua alvo, automaticamente seleciona um alvo novo
    public void OnTargetDeath() {
        if (target == null) {
            TargetNext();
        } 
    }
    
    public Character ReturnTarget()
    {
        return target;
    }
}
