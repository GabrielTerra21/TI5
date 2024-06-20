using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ECombatState : State {
    [Space(10)][Header("Components")]
    public Character self;
    public EngageSphere eDetect;
    public SkillContainer sc;
    [SerializeField] private NavMeshAgent ai;
    
    [Space(10)][Header("Data")]
    public Character target;
    [SerializeField] private bool Moving, casting, rebound;
    [SerializeField] private float reboundTime, reboundTimer;
    public EnemyPack ePack;

    [Space(10)] [Header("Animation Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private int animationLayerIndex;
    
    


    private void Awake() {
        if(!self) self = GetComponent<Character>();
        animationLayerIndex = animator.GetLayerIndex("Combat");
        if ( !sc ) sc = GetComponent<SkillContainer>();
        if ( !eDetect ) eDetect = GetComponent<EngageSphere>();
    }
    
    private void FixedUpdate() {
        if(paused) return;
        if (rebound) {
            if (InDistance(sc.autoAttack, target.transform)) {
                Debug.Log("AutoAttacking");
                sc.AutoAttack(target);
                Moving = false;
            }
            else if (!Moving) {
                Moving = true;
                Debug.Log("Moving has failed");
                animator.SetFloat("MovementY", self.moveSpeed);
                StartCoroutine(Movement(sc.autoAttack));
            }

            reboundTimer -= Time.fixedDeltaTime;
            if (reboundTimer <= 0) {
                rebound = false;
                reboundTimer = reboundTime;
            }
        } 
        else if(!casting) SelectAttack();
    }
    
    public override State OnEnterState() {
        animator.SetLayerWeight(animationLayerIndex, 1);
        Aggro(eDetect.GetNextTarget());
        return this;
    }

    public override void OnExitState() {
        Debug.Log($"{gameObject.name} is exiting combat state");
        animator.SetLayerWeight(animationLayerIndex, 0);
        target = null;
        StopAllCoroutines();
    }

    public void Aggro(Character target) { this.target = target; }

    public void Alert() {
        
    }
    
    public void SelectAttack() {
        if(casting) return;
        foreach (var data in sc.skills) {
            if (data.ready) {
                StartCoroutine(MakeAttack(data));
                return;
            }
        }
    }

    public void Cast(Skill skill) => sc.Cast(skill, target); 

    /*public void Cast(int index) => sc.Cast(index, target);*/ 

    // public bool AttackReady(Skill skill) { return (skill.ready); }

    IEnumerator MakeAttack(Skill selected) {
        Debug.Log($"{selected.data.SkillName} chosen");
        if (casting) {
            Debug.Log("Casting overlap, breaking new casting");
            yield break;
        }
        casting = true;
        if (selected.data.Range != 0 &&!InDistance(selected, target.transform)) {
            Moving = true;
            //movement.Moving(target, selected);
            StartCoroutine(Movement(selected));
            Debug.Log("Make Attack failed");
            animator.SetFloat("MovementY", self.moveSpeed );
            Debug.Log("moving in range");
            yield return new WaitUntil(() => InDistance(selected, target.transform));
            Debug.Log("Reached range for attack");
            Moving = false;
        }
        Debug.Log($"Casting {selected.data.SkillName}"); 
        Cast(selected);
        Debug.Log($"{selected.data.SkillName} casted successfully");
        casting = false;
        rebound = true;
    }
    
    public bool InDistance(Skill skill, Transform targetPos) {
        if ((targetPos.position - transform.position).sqrMagnitude > skill.data.Range * skill.data.Range) {
            Debug.Log($"Not in distance : {skill.data.Range}");
            return false;
        }
        Debug.Log("in distance");
        animator.SetFloat("MovementY", 0);
        return true;
    }
    IEnumerator Movement ( Skill skill ) {
        Vector3 targetPos = target.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(targetPos, out hit, skill.data.Range - 1 , -1); 
        Vector3 desiredPos = hit.position;
        ai.SetDestination(desiredPos);
        while (!InDistance(skill, target.transform)) {
            if (target.transform.position != targetPos) {
                StartCoroutine(Movement(skill));
                yield break;
            }
            yield return null;
        }
        ai.ResetPath();
        Debug.Log("New destination set");
    }
    
}
