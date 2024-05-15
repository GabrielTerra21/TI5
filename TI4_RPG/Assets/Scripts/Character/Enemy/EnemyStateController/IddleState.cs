using System.Collections; 
using UnityEngine; 
using UnityEngine.AI; 
using Random = UnityEngine.Random; 
 
public class IddleState : State { 
    [SerializeField] private Vector3 homePos = Vector3.zero; 
    [SerializeField] private Vector3 newPos; 
    [SerializeField] private NavMeshAgent ai; 
    [SerializeField] private float roamingDistance, reachingDistance, idleTimeLimit, idleTimer; 
    [SerializeField] private bool ready; 
 
 
    private void Awake() { 
        if (!ai) ai = GetComponent<NavMeshAgent>(); 
        if (homePos == Vector3.zero) homePos = transform.position; 
    } 
 
    private void FixedUpdate() { 
        Roaming(); 
    } 
 
    public override State OnEnterState() { 
         
        return this; 
    } 
 
    public override void OnExitState() { 
        StopAllCoroutines(); 
    } 
 
    public void Roaming() { 
        if (idleTimer <= 0 && ready) { 
            // Vector2 targetPos; 
            // targetPos = new Vector2(Random.Range(-roamingDistance, roamingDistance), Random.Range(-reachingDistance, roamingDistance)); 
            // Mathf.Clamp(targetPos.sqrMagnitude, -roamingDistance * roamingDistance, roamingDistance * roamingDistance); 
            // newPos = homePos + new Vector3(targetPos.x, 0, targetPos.y); 
            // ai.SetDestination(newPos); 
            // StartCoroutine(Walking()); 
            StartCoroutine(Walking()); 
        } 
        if (ready) idleTimer -= Time.fixedDeltaTime; 
    } 
 
    IEnumerator Walking() { 
        ready = false; 
         
        Vector2 targetPos = new Vector2(Random.Range(-roamingDistance, roamingDistance), Random.Range(-roamingDistance, roamingDistance)); 
        Mathf.Clamp(targetPos.sqrMagnitude, -roamingDistance * roamingDistance, roamingDistance * roamingDistance); 
        newPos = homePos + new Vector3(targetPos.x, 0, targetPos.y); 
        ai.SetDestination(newPos); 
         
        yield return new WaitUntil(() =>(transform.position - newPos).sqrMagnitude < reachingDistance * reachingDistance); 
        idleTimer = Random.Range(0, idleTimeLimit); 
        ready = true; 
    } 
} 