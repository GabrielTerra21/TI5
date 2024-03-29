using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterDataSO data;
    public int curHp;
    public Animator animator;

    protected virtual void Awake(){
        curHp = data.MaxHp;
    }

    public virtual void TakeDamage(int dmg){
        curHp -= dmg;
        if(curHp <= 0 ) Die();
    }

    public abstract void Die();
}
