using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    
    public override void Die() {
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}
