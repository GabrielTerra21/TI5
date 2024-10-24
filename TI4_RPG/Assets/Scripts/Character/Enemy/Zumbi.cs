using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Zumbi : State
{
    [Space(10)]
    [Header("Components")]
    [SerializeField] private Character self;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent ai;

    private enum BEHAVIOUR
    {
        ATTACK,
        IDDLE
    }

    [Space(10)]
    [Header("Info")]
    [SerializeField] private BEHAVIOUR behaviour;
    [SerializeField] private SkillDataSO primarySkill, secondarySkill;
    [SerializeField] private Character target;
    [SerializeField] private int animationMovementID, animationPrimeryAttID, animationSecundaryAttID;
    [SerializeField] private float iddleTime = 3;
    private float _iddleTimer;
    [SerializeField] private bool moving = false;
    private bool cooldown = true;
    private float timer;


    // Adquire referencia da layer de anima��o e componentes
    private void Awake()
    {
        paused = true;
        ai = GetComponent<NavMeshAgent>();
        ai.speed = self.moveSpeed;
        gameObject.SetActive(false);
        animationMovementID = Animator.StringToHash("IsWalking");
        animationPrimeryAttID = Animator.StringToHash("IsAttackS");
        animationSecundaryAttID = Animator.StringToHash("IsAttackC");
    }

    private void OnEnable()
    {
        OnEnterState();
        paused = true;
    }

    private void OnDisable()
    {
        OnExitState();
    }

    // Ativa a layer de anima��o adequada, busca um alvo do tipo Character
    // retorna this para que o StateManager saiba o estado atual do personagem
    public override State OnEnterState()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Character>();
        GameManager.Instance.CallCombatMode();
        return this;
    }

    // Desliga layer de anima��o de combate, limpa variavel de alvo
    // e para quaisquer coroutinas ainda sendo executadas neste MonoBehaviour
    public override void OnExitState()
    {
        target = null;
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (paused) return;
        CoolDown();
        // Toda a logica de Comportamento do inimigo de acordo com o behaviour STATE
        switch (behaviour)
        {
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:
                // Caso o alvo esteja dentro de alcance, a skill � conjurada
                // e o comportamento de IDDLE iniciado.
                if (InDistance(primarySkill, target.transform))
                {
                    primarySkill.OnCast(self, target);
                    _iddleTimer = iddleTime;
                    behaviour = BEHAVIOUR.IDDLE;
                    animator.SetBool(animationPrimeryAttID,true);
                }
                else if(InDistance(secondarySkill, target.transform) && Random.Range(0,10) > 8 && cooldown)
                {
                    secondarySkill.OnCast(self, target);
                    _iddleTimer = iddleTime;
                    behaviour = BEHAVIOUR.IDDLE;
                    cooldown = false;
                    timer = 0;
                    animator.SetBool(animationSecundaryAttID, true);
                }
                // Caso o alvo n�o esteja dentro de alcance
                // e o agente n�o esteja se movendo, inicia movimento em dire��o ao alvo.
                else if (!moving)
                {
                    StartCoroutine(Movement(primarySkill));
                    animator.SetBool(animationMovementID,true);
                }
                transform.LookAt(target.transform.position);
                break;
            // Faz uma contagem regressiva para retornar ao estado de ATTACK.
            // Serve unicamente para impedir o que o agente ataque initerruptamente.
            case BEHAVIOUR.IDDLE:
                animator.SetBool(animationMovementID, false);
                animator.SetBool(animationSecundaryAttID, false);
                animator.SetBool(animationPrimeryAttID, false);
                _iddleTimer -= Time.fixedDeltaTime;
                if (_iddleTimer <= 0)
                {
                    behaviour = BEHAVIOUR.ATTACK;
                }
                break;
        }
    }

    // Diz se o alvo est� ou n�o dentro do alcance da skill fornecida como argumento
    public bool InDistance(SkillDataSO skill, Transform targetPos)
    {
        if ((targetPos.position - transform.position).sqrMagnitude > skill.Range * skill.Range)
        {
            return false;
        }
        return true;
    }

    // Utiliza a variavel range da skill fornecida como argumento para estabelecer uma posi��o
    // desejavel para o agente, em rela��o ao alvo, que permita conjurar o ataque, e � usa como
    // destino para o NavMesh agent do agente.
    IEnumerator Movement(SkillDataSO skill)
    {
        moving = true;
        Vector3 targetPos = target.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(targetPos, out hit, skill.Range - 1, -1);
        Vector3 desiredPos = hit.position;
        ai.SetDestination(desiredPos);
        // Enquanto estiver fora de alcance
        // atualiza valor de anima��o e checa se o alvo mudou de posi��o.
        while (!InDistance(skill, target.transform) && behaviour != BEHAVIOUR.IDDLE)
        {
            // Caso o alvo tenha mudado de posi��o, inicia uma nova coroutina de movimenta��o.
            if (target.transform.position != targetPos)
            {
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
