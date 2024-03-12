using UnityEngine;

public abstract class AnimationController
{
    public Player agent;


    public AnimationController(Player agent){
        this.agent = agent;
    }   

    public abstract void SetAnimations();
}
