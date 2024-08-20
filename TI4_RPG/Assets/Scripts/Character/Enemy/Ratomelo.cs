using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Ratomelo : State {
    [Space(10)] [Header("Components")] 
    [SerializeField] private Character self;
    [SerializeField] private Animator animator;
    [SerializeField] private EngageSphere eDetect;
    [SerializeField] private NavMeshAgent ai;

    private enum BEHAVIOUR {
        ATTACK,
        IDDLE
    }
    
    [Space(10)] [Header("Info")] 
    [SerializeField] private BEHAVIOUR behaviour;
    [SerializeField] private SkillDataSO skill;
    [SerializeField] private Character target;
    [SerializeField] private int animationLayerIndex;
    [SerializeField] private int animationMovementID;
    [SerializeField] private float iddleTime = 3;
    private float _iddleTimer;
    [SerializeField] private bool moving = false;


    // Adquire referencia da layer de animação e componentes
    private void Awake() {
        if (animator != null) animationLayerIndex = animator.GetLayerIndex("Combat");
        animationMovementID = Animator.StringToHash("Movement");
        if(eDetect == null) eDetect = GetComponent<EngageSphere>();
        ai = GetComponent<NavMeshAgent>();
        ai.speed = self.moveSpeed;
    }
    
    // Ativa a layer de animação adequada, busca um alvo do tipo Character
    // retorna this para que o StateManager saiba o estado atual do personagem
    public override State OnEnterState() {
        if(animator != null) animator.SetLayerWeight(animationLayerIndex, 1);
        target = eDetect.GetNextTarget();
        return this;
    }

    // Desliga layer de animação de combate, limpa variavel de alvo
    // e para quaisquer coroutinas ainda sendo executadas neste MonoBehaviour
    public override void OnExitState() {
        Debug.Log($"{gameObject.name} is exiting combat state");
        if(animator != null) animator.SetLayerWeight(animationLayerIndex, 0);
        target = null;
        StopAllCoroutines();
    }
    
    private void FixedUpdate() {
        if(paused) return;

        // Toda a logica de Comportamento do inimigo de acordo com o behaviour STATE
        switch (behaviour) {
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:
                // Caso o alvo esteja dentro de alcance, a skill é conjurada
                // e o comportamento de IDDLE iniciado.
                if (InDistance(skill, target.transform)) { 
                    skill.OnCast(self, target);
                    _iddleTimer = iddleTime;
                    behaviour = BEHAVIOUR.IDDLE;
                }
                // Caso o alvo não esteja dentro de alcance
                // e o agente não esteja se movendo, inicia movimento em direção ao alvo.
                else if (!moving) {
                    Debug.Log("Starting Movement");
                    StartCoroutine(Movement(skill));
                }
                break;
            // Faz uma contagem regressiva para retornar ao estado de ATTACK.
            // Serve unicamente para impedir o que o agente ataque initerruptamente.
            case BEHAVIOUR.IDDLE:
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
        animator.SetFloat(animationMovementID, ai.speed / self.moveSpeed);
        Vector3 targetPos = target.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(targetPos, out hit, skill.Range - 1, -1); 
        Vector3 desiredPos = hit.position;
        ai.SetDestination(desiredPos);
        // Enquanto estiver fora de alcance
        // atualiza valor de animação e checa se o alvo mudou de posição.
        Debug.Log("Destination Set");
        while (!InDistance(skill, target.transform)) {
            // Caso o alvo tenha mudado de posição, inicia uma nova coroutina de movimentação.
            if (target.transform.position != targetPos) {
                StartCoroutine(Movement(skill));
                yield break;
            }
            yield return null;
        }
        animator.SetFloat(animationMovementID, 0);
        ai.ResetPath();
        moving = false;
        Debug.Log("New destination set");
    }
}
