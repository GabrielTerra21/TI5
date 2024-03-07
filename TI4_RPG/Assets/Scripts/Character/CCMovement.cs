using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CCMovement
{
    public CharacterController cc;
    public float moveSpeed;


    public CCMovement(Player agent){
        cc = agent.cc;
        moveSpeed = agent.moveSpeed;
    }

    public void Moving(Vector2 moveDir) => cc.Move(new Vector3(moveDir.x, 0, moveDir.y) * moveSpeed * Time.deltaTime);


}
