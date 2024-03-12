using UnityEngine;

public class ExploringState : PlayerState
{
    AnimationController animation;
    IMovement movement;


    public ExploringState(Player agent) : base(agent){
        animation = new DefaultController(agent);
        movement = new CCMovement(agent);
    }

    public override void OnEnterState()
    {
        agent.animationController = animation;
        agent.movement = movement;
        Debug.Log("Entering exploration state.");
    }
}
