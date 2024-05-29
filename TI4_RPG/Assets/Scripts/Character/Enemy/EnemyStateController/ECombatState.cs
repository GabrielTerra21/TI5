using System;

using UnityEngine;
using UnityEngine.AI;

public class ECombatState : State {
    public Character target;
    public EngageSphere eDetect;
    public SkillContainer sc;
    public InRangeMovement movement;
    public bool inAction;
    public EnemyPack ePack;


    private void Awake() {
        movement = new InRangeMovement(GetComponent<NavMeshAgent>());
        if ( !sc ) sc = GetComponent<SkillContainer>();
        if ( !eDetect ) eDetect = GetComponent<EngageSphere>();
        if (ePack) ePack.attack.AddListener(Aggro);
    }
    
    private void FixedUpdate() {
        if (AttackReady(sc.autoAttack)) {
            if (InDistance(sc.autoAttack, target.transform)) {
                Debug.Log("AutoAttacking");
                sc.AutoAttack(target);
                inAction = false;
            }
            else if(!inAction) {
                inAction = true;
                Debug.Log("Moving");
                movement.Moving(target, sc.autoAttack);
            }
        }
    }
    
    public override State OnEnterState() {
        Debug.Log($"{gameObject.name} has entered combat state");
        if (ePack) {
            ePack.attack.Invoke(target);
            return this;
        }
        Debug.Log("here");
        Aggro(eDetect.GetNextTarget());
        return this;
    }

    public override void OnExitState() {
        Debug.Log($"{gameObject.name} is exiting combat state");
        target = null;
        StopAllCoroutines();
    }

    public void Aggro(Character target) {
        this.target = target;
    }

    public void Cast(Skill skill) {
        sc.Cast(skill, target);
    }

    public void Cast(int index) {
        sc.Cast(index, target);
    }

    public bool AttackReady(Skill skill) {
        if (skill.ready) return true;
        else return false;
    }

    public bool InDistance(Skill skill, Transform targetPos) {
        if ((targetPos.position - transform.position).sqrMagnitude > skill.data.Range * skill.data.Range) {
            Debug.Log("Not in distance");
            return false;
        }
        return true;
    }

    // public void AutoAttack() {
    //     if (Vector3.Distance(transform.position, target.transform.position) > sc.autoAttack.data.Range && !inAction) StartCoroutine(GetInRange(sc.autoAttack));
    //     else {
    //         Debug.Log("In range");
    //         sc.AutoAttack(target);
    //     }
    // }

    /*IEnumerator GetInRange(Skill skill) {
        inAction = true;
        Debug.Log("Get in range start");
        Vector3 offset = target.transform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        float desiredDistance = skill.data.Range * skill.data.Range;
        desiredDistance *= .95f;
        Debug.Log("Calculation complete");
        if (desiredDistance  < sqrLen) {
            Debug.Log("Initiating movement");
            movement.Moving(new Vector2(target.transform.position.x, target.transform.position.z), skill.data.Range);
            yield return new WaitUntil(() => desiredDistance >= (target.transform.position - transform.position).sqrMagnitude);
            Debug.Log("Attacking range achieved");
        }
        Debug.Log("Skill casted successfully, coroutine ended");
        inAction = false;
        Debug.Log("GetInRange has ended");
        yield break;
    }*/
}
