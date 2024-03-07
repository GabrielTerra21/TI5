public class ExploringState : PlayerState
{
    public override void OnEnterState(Player agent)
    {
        agent.animationController = new AnimationController(agent);
        agent.movement = new CCMovement(agent);
    }
}
