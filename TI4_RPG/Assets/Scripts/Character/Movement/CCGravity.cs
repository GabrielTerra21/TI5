using UnityEngine;

public class CCGravity : GroundDetect
{
    public CharacterController cc;
    const float gravity = 9.81f;
    public bool falling;
    public float gravityMod = 1;
    [SerializeField] private Vector3 gravityAcc = new Vector3(0, 0, 0);


    private void Awake() => cc = GetComponent<CharacterController>();

    public void Gravity(){
        if(!IsGrounded()){
            gravityAcc.y -= gravity * gravityMod * Time.fixedDeltaTime;
            falling = true;
        }
        else{
            gravityAcc = Vector3.zero;
            falling = false;
        }
        cc.Move(gravityAcc);
    }
}
