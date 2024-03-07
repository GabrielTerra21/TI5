using UnityEngine;

public class CCGravity : GroundDetect
{
    public CharacterController cc;
    const float gravity = 9.81f;
    public float gravityMod = 1;
    [SerializeField] private Vector3 gravityAcc = new Vector3(0, 0, 0);


    private void Awake() => cc = GetComponent<CharacterController>();

    public void Gravity(){
        if(!IsGrounded()){
            gravityAcc.y -= gravity * gravityMod * Time.fixedDeltaTime;
        }
        else{
            gravityAcc = Vector3.zero;
        }
        cc.Move(gravityAcc);
    }
}
