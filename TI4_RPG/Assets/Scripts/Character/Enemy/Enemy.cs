using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    
    public Vector3 homePos = Vector3.zero;
    public float roamingDistance;
    public float reachingDistance = 0.2f;
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
    protected override void Start() {
        base.Start();
        homePos = transform.position;
    }
    
    public override void Die() {
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    /*
    IEnumerator HitStop() {
        GameManager.Instance.PauseGame();
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.UnpauseGame();
    }
    */
}
