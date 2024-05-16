using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;

    public UnityEvent OnDeath, OnDamage;

    [Space(5)]
    [Header("Character Sheet")]
    public CharacterDataSO data;
    public float moveSpeed;
    public int life;
    public int defense { get; private set; }
    public int attack { get; private set; }
    public bool actionable = true;

    protected virtual void Awake() {
        GetData();
    }

    public void GetData(){
        moveSpeed = data.moveSpeed;
        life = data.maxHp;
        defense = data.armor;
        attack = data.power;
    }

    public virtual int TakeDamage(int dmg) {
        Debug.Log("Taking Damgae");
        life -= dmg;
        OnDamage.Invoke();
        if (life <= 0) {
            life = 0;
            Die();
        }
        Debug.Log("ouchie");
        return dmg;
    }

    public virtual void Heal(int heal) {
        life += heal;
        if (life > data.maxHp) life = data.maxHp;
    }

    public abstract void Die();

}
