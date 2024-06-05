using System.Collections;
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
         NavMesh.SamplePosition(target.transform.position, out hit, skill.data.Range - 1 , -1); 
         Vector3 desiredPos = hit.position;
        // Vector3 desiredPos = target.transform.position - agent.transform.position;
        // desiredPos = Vector3.ClampMagnitude(desiredPos, desiredPos.magnitude - (skill.data.Range - 1));
        agent.SetDestination(desiredPos);
        Debug.Log("New destination set");
    }

    // IEnumerator Movement(Character target, Skill skill ) {
    //     NavMeshHit hit;
    //     NavMesh.SamplePosition(target.transform.position, out hit, skill.data.Range - 1 , -1); 
    //     Vector3 desiredPos = hit.position;
    //     agent.SetDestination(desiredPos);
    //     while
    //     Debug.Log("New destination set");
    // }
}
