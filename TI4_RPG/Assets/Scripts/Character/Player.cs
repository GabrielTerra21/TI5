using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Vector2 moveDir = new Vector2();
    public CharacterController cc;
    public CCGravity gravity;
    public Animator animator;
    public AnimationController animationController;
    public CCMovement movement;

    #region Unity default methods
    
    private void Awake(){
        cc = GetComponent<CharacterController>();
        gravity = GetComponent<CCGravity>();
    }
    private void Start(){
        animationController = new AnimationController(this);
        movement = new CCMovement(this);
    }

    private void Update(){
        movement.Moving(moveDir);
        animationController.WalkAnimation();
    }

    private void FixedUpdate(){
        gravity.Gravity();
    }

    #endregion

    #region Input System Methods

    private void OnMovement(InputValue value){
        moveDir = value.Get<Vector2>();
    }

    #endregion
}
