using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    public CharacterController cc;
    public CCGravity gravity;
    public Vector2 moveDir = new Vector2();

    #region Unity default methods
    
    protected override void Awake(){
        base.Awake();
        cc = GetComponent<CharacterController>();
        gravity = GetComponent<CCGravity>();
    }

    private void Update(){
        movement.Moving(moveDir);
        animationController.SetAnimations();
    }

    private void FixedUpdate(){
        gravity.Gravity();
    }

    #endregion
    #region Methods

    public override void Die(){

    }

    #endregion
    #region Input System Methods

    private void OnMovement(InputValue value){
        moveDir = value.Get<Vector2>();
    }

    #endregion
}
