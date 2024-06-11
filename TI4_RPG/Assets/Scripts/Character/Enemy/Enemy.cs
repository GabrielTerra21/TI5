using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    
    public Vector3 homePos = Vector3.zero;
    public float roamingDistance, reachingDistance;
    public NavMeshAgent ai;

    protected override void Awake() {
        base.Awake();
        ai = GetComponent<NavMeshAgent>();
    }

    public override void Pause() {
        base.Pause();
        ai.isStopped = true;
    }

    public override void Unpause() {
        base.Unpause();
        ai.isStopped = false;
    }
    private void Start() {
        if (homePos == Vector3.zero) homePos = transform.position;
        if (reachingDistance == 0) reachingDistance = .2f;
    }
    
    public override void Die() {
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}
