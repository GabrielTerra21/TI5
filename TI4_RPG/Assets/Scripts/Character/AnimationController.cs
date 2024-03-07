using UnityEngine;

public class AnimationController
{
    public Player agent;


    public AnimationController(Player agent){
        this.agent = agent;
    }   

    public void WalkAnimation(){
        agent.animator.SetFloat("Movement", agent.moveDir.magnitude);

        //Look at movement direction
        agent.transform.LookAt(agent.transform.position + new Vector3(agent.moveDir.x, 0, agent.moveDir.y));
    }

}
