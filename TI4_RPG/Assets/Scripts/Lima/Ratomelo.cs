using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Ratomelo : State {
    [Space(10)] [Header("Components")] 
    [SerializeField] private Character self;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent ai;

    private enum BEHAVIOUR {
        ATTACK,
        IDDLE
    }
    
    [Space(10)] [Header("Info")] 
    [SerializeField] private BEHAVIOUR behaviour;
    [SerializeField] private SkillDataSO autoAttack,secondarySkill;
    [SerializeField] private int animationMovementID, animationPrimeryAttID;
    [SerializeField] private Character target;
    [SerializeField] private float iddleTime;
    private float _iddleTimer;
    [SerializeField] private bool moving = false;
    private bool cooldown = true;
    private float timer;



    // Adquire referencia da layer de animação e componentes
    private void Awake() {
        paused = true;
        ai = GetComponent<NavMeshAgent>();
        ai.speed = self.moveSpeed;
        gameObject.SetActive(false);
        animationMovementID = Animator.StringToHash("IsWalking");
        animationPrimeryAttID = Animator.StringToHash("IsAttack");
    }

    private void OnEnable() {
        OnEnterState();
        paused = true;
    }

    private void OnDisable() {
        OnExitState();
    }
    
    // Ativa a layer de animação adequada, busca um alvo do tipo Character
    // retorna this para que o StateManager saiba o estado atual do personagem
    public override State OnEnterState() {
        target = GameObject.FindWithTag("Player").GetComponent<Character>();
        GameManager.Instance.CallCombatMode();
        return this;
    }

    // Desliga layer de animação de combate, limpa variavel de alvo
    // e para quaisquer coroutinas ainda sendo executadas neste MonoBehaviour
    public override void OnExitState() {
        target = null;
        StopAllCoroutines();
    }
    
    private void FixedUpdate() {
        if(paused) return;
        CoolDown();
        // Toda a logica de Comportamento do inimigo de acordo com o behaviour STATE
        switch (behaviour) {
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:
                // Caso o alvo esteja dentro de alcance, a skill é conjurada
                // e o comportamento de IDDLE iniciado.
                if(secondarySkill != null && InDistance(secondarySkill, target.transform) && cooldown)
                {
                    secondarySkill.OnCast(self, target);
                    animator.SetBool(animationPrimeryAttID, true);
                    _iddleTimer = iddleTime;
                    behaviour = BEHAVIOUR.IDDLE;
                    cooldown = false;
                    timer = 0;
                }
                else if (InDistance(autoAttack, target.transform)) { 
                    autoAttack.OnCast(self, target);
                    animator.SetBool(animationPrimeryAttID, true);
                    _iddleTimer = iddleTime;
                    behaviour = BEHAVIOUR.IDDLE;
                }
                // Caso o alvo não esteja dentro de alcance
                // e o agente não esteja se movendo, inicia movimento em direção ao alvo.
                else if (!moving) {
                    StartCoroutine(Movement(autoAttack));
                    animator.SetBool(animationMovementID, true);
                }
                transform.LookAt(target.transform.position);
                break;
            // Faz uma contagem regressiva para retornar ao estado de ATTACK.
            // Serve unicamente para impedir o que o agente ataque initerruptamente.
            case BEHAVIOUR.IDDLE:
                animator.SetBool(animationMovementID, false);
                animator.SetBool(animationPrimeryAttID, false);
                _iddleTimer -= Time.fixedDeltaTime;
                if (_iddleTimer <= 0) {
                    behaviour = BEHAVIOUR.ATTACK;
                }
                break;
        }
    }
    
    // Diz se o alvo está ou não dentro do alcance da skill fornecida como argumento
    public bool InDistance(SkillDataSO skill, Transform targetPos) {
        if ((targetPos.position - transform.position).sqrMagnitude > skill.Range * skill.Range) {
            return false;
        }
        return true;
    }
    
    // Utiliza a variavel range da skill fornecida como argumento para estabelecer uma posição
    // desejavel para o agente, em relação ao alvo, que permita conjurar o ataque, e à usa como
    // destino para o NavMesh agent do agente.
    IEnumerator Movement ( SkillDataSO skill ) {
        moving = true;
        Vector3 targetPos = target.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(targetPos, out hit, skill.Range - 1, -1); 
        Vector3 desiredPos = hit.position;
        ai.SetDestination(desiredPos);
        // Enquanto estiver fora de alcance
        // atualiza valor de animação e checa se o alvo mudou de posição.
        while (!InDistance(skill, target.transform)) {
            // Caso o alvo tenha mudado de posição, inicia uma nova coroutina de movimentação.
            if (target.transform.position != targetPos) {
                StartCoroutine(Movement(skill));
                yield break;
            }
            yield return null;
        }
        ai.ResetPath();
        moving = false;
    }
    public void CoolDown()
    {
        if (!cooldown)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= secondarySkill.CoolDown)
            {
                cooldown = true;
            }
        }
    }
}
