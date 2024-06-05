using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ECombatState : State {
    [Space(10)][Header("Components")]
    public Character self;
    public EngageSphere eDetect;
    public SkillContainer sc;
    
    [Space(10)][Header("Data")]
    public Character target;
    [SerializeField] private bool Moving, casting, rebound;
    [SerializeField] private float reboundTime, reboundTimer;
    private InRangeMovement movement;
    public EnemyPack ePack;

    [Space(10)] [Header("Animation Components")]
    [SerializeField] private Animator animator;
    private AnimationController ac;
    [SerializeField] private int animationLayerIndex;
    
    


    private void Awake() {
        if(!self) self = GetComponent<Character>();
        ac = new CombatController(animator);
        animationLayerIndex = animator.GetLayerIndex("Combat");
        movement = new InRangeMovement(GetComponent<NavMeshAgent>());
        if ( !sc ) sc = GetComponent<SkillContainer>();
        if ( !eDetect ) eDetect = GetComponent<EngageSphere>();
        if (ePack) ePack.attack.AddListener(Aggro);
    }
    
    private void FixedUpdate() {
        if (rebound) {
            if (InDistance(sc.autoAttack, target.transform)) {
                Debug.Log("AutoAttacking");
                sc.AutoAttack(target);
                Moving = false;
            }
            else if (!Moving) {
                Moving = true;
                Debug.Log("Moving");
                animator.SetFloat("MovementY", self.moveSpeed);
                movement.Moving(target, sc.autoAttack);
            }

            reboundTimer -= Time.fixedDeltaTime;
            if (reboundTimer <= 0) {
                rebound = false;
                reboundTimer = reboundTime;
            }
        } 
        else if(!casting && !Moving) SelectAttack();
    }
    
    public override State OnEnterState() {
        Debug.Log($"{gameObject.name} has entered combat state");
        if (ePack) {
            ePack.attack.Invoke(target);
            return this;
        }
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
            movement.Moving(target, selected);
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
    
}
