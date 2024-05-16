using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ECombatState : State {
    public Character target;
    public EngageSphere eDetect;
    public SkillContainer sc;
    public IMovement movement;
    public bool inAction;


    private void Awake() {
        movement = new InRangeMovement(GetComponent<NavMeshAgent>());
        if ( !sc ) sc = GetComponent<SkillContainer>();
        if ( !eDetect ) eDetect = GetComponent<EngageSphere>();
    }

    public void Start() {
        inAction = false;
    }

    private void FixedUpdate() {
        AutoAttack();
    }
    
    public override State OnEnterState() {
        Debug.Log($"{gameObject.name} has entered combat state");
        target = eDetect.GetNextTarget();
        if (target == null) {
            throw new Exception($"{gameObject.name} has entered state without a target");
        }
        return this;
    }

    public override void OnExitState() {
        Debug.Log($"{gameObject.name} is exiting combat state");
        target = null;
        StopAllCoroutines();
    }

    public void AutoAttack() {
        sc.AutoAttack(target);
        if (Vector3.Distance(transform.position, target.transform.position) > sc.autoAttack.data.Range && !inAction) StartCoroutine(GetInRange(sc.autoAttack));
    }

    IEnumerator GetInRange(Skill skill) {
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
    }
}
