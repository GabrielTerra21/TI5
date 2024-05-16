using UnityEngine;

public class Enemy : Character {
    
    public Vector3 homePos = Vector3.zero;
    public float roamingDistance, reachingDistance;


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
