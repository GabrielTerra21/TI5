using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CombatState : State {
    [Space(10)] [Header("State Components")] 
    [SerializeField] private SplineLine line;
    [SerializeField] private AttackIndicator aoe;
    [SerializeField] private Material aoeMat;
    public Character agent;
    [SerializeField] private Character target;
    [SerializeField] private EngageSphere eDetect;
    //public SkillContainer skillManager;

    [Space(10)]
    [Header("New Combat System Components")]
    //public Action OnEndCombat;
    [SerializeField] private List<Character> enemies = new List<Character>();
    [SerializeField] SkillDataSO autoAttack;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] float coolDown;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private bool dashReady = true;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private AutoAttackGear gearIcon;
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
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Dash").performed += Dash;
        
    }

    //Desubscreve as respectivas ações ao mapa de ações do input Manager
    private void OnDisable() {
        if (InputManager.Instance == null) return;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").started -= OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").performed -= OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Movement").canceled -= OnMovement;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("SwitchEnemy").performed -= TargetNext;
        InputManager.Instance.actions.FindActionMap("Combat").FindAction("Dash").performed -= Dash;
        moveDir = Vector3.zero;
    }

    private void Update()
    {
        if(paused) return;
        if(!agent.actionable) return;
        movement.Moving(moveDir, agent.moveSpeed);
        
        if(target == null) return;
        
        //targetLock.SetRotation(target.transform.position);
        SetRotation();
        if(InRange(target.transform))
        {
            if (moveDir.magnitude < 0.05f && coolDown >= autoAttack.CoolDown) {
                autoAttack.OnCast(agent, target);
                //animator.SetTrigger("Attack");
                coolDown = 0;
            }
        }
        coolDown += Time.deltaTime;
        gearIcon.UpdateGear(coolDown/ autoAttack.CoolDown);
    }

    private void SetRotation() {
        /*
        if (Vector3.Distance(transform.position, target.transform.position) < autoAttack.Range){
            transform.LookAt(target.transform.position);
            animationController.SetAnimations(moveDir); 
        }
        else {
            transform.LookAt(agent.transform.position + new Vector3(moveDir.x, 0, moveDir.y));
            animator.SetFloat("Movement", moveDir.magnitude);
        }
        */
        
        transform.LookAt(agent.transform.position + new Vector3(moveDir.x, 0, moveDir.y));
        animator.SetFloat("Movement", moveDir.magnitude);
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
        gearIcon.gameObject.SetActive(true);

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
        gearIcon.gameObject.SetActive(false);
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
    // Tenta encontrar um novo alvo automaticamente, em caso de falha, encerra o combate.
    public void TargetNext() {
        try {
            Character nTarget = enemies?.First(data => data != null);
            target = nTarget;
            line.Target(target.LockOnTarget);
        }
        catch {
             GameManager.Instance.CallExploration();
             return;
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

    private void Dash(InputAction.CallbackContext context) {
        if (moveDir != Vector2.zero && dashReady) {
            Vector3 inputDir = new Vector3(moveDir.x, 0, moveDir.y);
            Vector3 finalPos = new Vector3();
            finalPos = transform.position + inputDir.normalized * dashDistance;
            StartCoroutine(Dash( finalPos  - transform.position));
            StartCoroutine(DashCD());
        }
    }

    IEnumerator DashCD() {
        dashReady = false;
        float timer = 0;

        while (timer < dashCoolDown) {
            yield return new WaitUntil(() => !GameManager.Instance.paused);
            timer += Time.unscaledDeltaTime;
            aoe.GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", timer/dashCoolDown);
            yield return null;
        }

        dashReady = true;
    }

    IEnumerator Dash(Vector3 movement) {
        agent.actionable = false;
        dashTrail.enabled = true;
        
        float timer = 0;
        Vector3 step = (movement / dashDuration) * Time.unscaledDeltaTime;
        
        while (timer < dashDuration) {
            yield return new WaitUntil(() => !GameManager.Instance.paused);
            cc.Move(step);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        
        agent.actionable = true;
        yield return new WaitForSeconds(0.1f);
        dashTrail.enabled = false;
    }

    public void LearnSkill(SkillDataSO skill) {
        try {
            for (int i = 0; i < skills.Length; i++) {
                if (skills[i] == null) {
                    skills[i] = skill;
                    return;
                }
            }
        }
        catch {
            throw new Exception("Numero maximo de skills ja alcançado");
        }
    }
    public void FindEnemys()
    {
        GameObject[] enemyGroup = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.Clear();
        for (int i = 0; i < enemyGroup.Length; i++)
        {
            if (!enemies.Contains(enemyGroup[i].GetComponent<Character>()))
            {
                enemies.Add(enemyGroup[i].GetComponent<Character>());
            }          
        }
    }
}
