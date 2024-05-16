using UnityEngine;
using UnityEngine.AI;

public class InRangeMovement : IMovement{
    private NavMeshAgent agent;

    public InRangeMovement(NavMeshAgent agent) {
        this.agent = agent;
    }
    
    public void Moving(Vector2 targetPos, float range) {
        Debug.Log("Setting new destination...");
        NavMeshHit hit;
        NavMesh.SamplePosition(new Vector3(targetPos.x, 0, targetPos.y), out hit, range - 0.25f, -1);
        Vector3 desiredPos = hit.position;
        agent.SetDestination(desiredPos);
        Debug.Log("New destination set");
    }
}
