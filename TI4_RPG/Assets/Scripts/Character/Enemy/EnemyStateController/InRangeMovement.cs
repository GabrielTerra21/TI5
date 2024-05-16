using UnityEngine;
using UnityEngine.AI;

public class InRangeMovement {
    private NavMeshAgent agent;

    public InRangeMovement(NavMeshAgent agent) {
        this.agent = agent;
    }
    
    public void Moving(Character target, Skill skill) {
        Debug.Log("Setting new destination...");
        NavMeshHit hit;
        NavMesh.SamplePosition(target.transform.position, out hit, skill.data.Range , -1);
        Vector3 desiredPos = hit.position;
        agent.SetDestination(desiredPos);
        Debug.Log("New destination set");
    }
}
