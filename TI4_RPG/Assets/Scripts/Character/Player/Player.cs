using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    public CCGravity gravity; 
    [Space(5)]
    [Header("Player Info")]
    public AnimationController animationController;
    public IMovement movement;
    public Vector2 moveDir = new Vector2();

    
    protected override void Awake(){
        base.Awake();
        gravity = GetComponent<CCGravity>();
    }

    private void Update(){
        animationController.SetAnimations();
    }

    private void FixedUpdate(){
        gravity.Gravity();
    }

    // private void OnMovement(InputValue value){
    //     moveDir = value.Get<Vector2>();
    // }

    public override void Die(){}

}
