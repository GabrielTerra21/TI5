using System.Collections; 
using UnityEngine; 
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random; 
 
public class IddleState : State {
    [Space(10)][Header("State Components")]
    [SerializeField] private Enemy agent;
    [SerializeField] private NavMeshAgent ai;
    public IMovement movement;
    
    [Space(10)][Header("State Data")]
    [SerializeField] private float idleTimeLimit, idleTimer; 
    [SerializeField] private Vector3 DesiredPos;
    [SerializeField] private bool ready; 
 
 
    private void Awake() {
        movement = new RoamingMovement();
        if (!agent) agent = GetComponent<Enemy>();
        if (!ai) ai = GetComponent<NavMeshAgent>(); 
    }

    private void Start() {
        ai.speed = agent.moveSpeed;
    }
 
    private void FixedUpdate() { 
        Roaming(); 
    } 
 
    public override State OnEnterState() { 
        Debug.Log($"{gameObject.name} has entered Iddle state");
        return this; 
    } 
 
    public override void OnExitState() { 
        StopAllCoroutines(); 
    } 
 
    public void Roaming() {
        if (ready) {
            idleTimer -= Time.fixedDeltaTime;
            if (idleTimer <= 0) StartCoroutine(Walking());
        }
    }

    private Vector3 SampleNewPosition(Vector3 origin) {
        Vector2 randomDir = Random.insideUnitCircle * agent.roamingDistance;
        Vector3 finalDir = agent.homePos + new Vector3(randomDir.x, 0, randomDir.y);
        NavMeshHit hit;
        NavMesh.SamplePosition(finalDir, out hit, agent.roamingDistance, -1);
        return hit.position;
    }
 
    IEnumerator Walking() {
        // if (!ready) {
        //     Debug.Log("Multiple invocations of walking are occuring.");
        //     yield break;
        // }
        ready = false; 
        
        DesiredPos = SampleNewPosition(agent.homePos); 
        ai.SetDestination(DesiredPos);
         
        yield return new WaitUntil(() =>(transform.position - DesiredPos).sqrMagnitude < agent.reachingDistance * agent.reachingDistance); 
        idleTimer = Random.Range(0, idleTimeLimit); 
        ready = true;
    } 
} 