using UnityEngine;

public class RoamingMovement : IMovement
{
    public void Moving(Vector2 homePos, float range) {
        
    }
    
    /*public void Roaming() {
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
        if (!ready) {
            Debug.Log("Multiple invocations of walking are occuring.");
            yield break;
        }
        ready = false; 
        
        DesiredPos = SampleNewPosition(agent.homePos); 
        ai.SetDestination(DesiredPos);
         
        yield return new WaitUntil(() =>(transform.position - DesiredPos).sqrMagnitude < agent.reachingDistance * agent.reachingDistance); 
        idleTimer = Random.Range(0, idleTimeLimit); 
        ready = true;
    } */
}
