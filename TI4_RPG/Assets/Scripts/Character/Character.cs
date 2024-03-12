using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int maxHp = 1;
    public int curHp;
    public float moveSpeed = 1.0f;
    public Animator animator;
    public IMovement movement;
    public AnimationController animationController;

    protected virtual void Awake(){
        curHp = maxHp;
    }

    public virtual void TakeDamage(int dmg){
        curHp -= dmg;
        if(curHp <= 0 ) Die();
    }

    public abstract void Die();
}
