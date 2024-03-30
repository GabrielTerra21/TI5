using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CCMovement : IMovement
{
    public CharacterController cc;

    public CCMovement(CharacterController cc){
        this.cc = cc;
    }

    public void Moving(Vector2 moveDir, float moveSpeed) => cc.Move(new Vector3(moveDir.x, 0, moveDir.y) * moveSpeed * Time.deltaTime);


}
