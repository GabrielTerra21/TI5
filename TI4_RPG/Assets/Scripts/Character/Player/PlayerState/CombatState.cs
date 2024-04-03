using UnityEngine;

public class CombatState 
{
    [Header("StateProperties")]
    public Character agent;
    public IMovement movement;
    public AnimationController animationController;
    public CharacterController cc;
    public Animator animator;
    public Vector2 moveDir;
}
