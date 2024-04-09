using UnityEngine;

public abstract class GroundDetect : MonoBehaviour
{
    [SerializeField] protected Vector3 boxSize;
    [SerializeField] protected float castDistance;
    [SerializeField] protected LayerMask hitLayer;

    public virtual bool IsGrounded(){
        bool onGround = Physics.BoxCast(transform.position + Vector3.up, boxSize, Vector3.down, transform.rotation, castDistance, hitLayer);
        return onGround;
    }
}
