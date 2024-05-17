using UnityEngine;
using UnityEngine.Events;

public class EnemyPack : MonoBehaviour {
    private SphereCollider col;
    public EnemyStateController[] children;
    public UnityEvent flee;
    public UnityEvent<Character> attack;
    public float maxDistance;


    private void Awake() {
        col = GetComponent<SphereCollider>();
    }

    private void Start() {
        foreach(var data in children)
        {
            flee.AddListener ( () => data.EnterState(data.Fleeing) );
        }
    }

    public void CheckDistance(float distance) {
        foreach (var data in children) {
            if((data.transform.position - transform.position).sqrMagnitude > maxDistance) flee?.Invoke();
        }
    }
}
