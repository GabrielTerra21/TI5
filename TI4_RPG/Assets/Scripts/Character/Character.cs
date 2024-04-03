using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;

    [Space(5)]
    [Header("Character Sheet")]
    public CharacterDataSO data;
    public float moveSpeed;
    public int life;
    public int defense;
    public int attack;

    protected virtual void Awake() {
        GetData();
    }

    public void GetData(){
        moveSpeed = data.moveSpeed;
        life = data.maxHp;
        defense = data.armor;
        attack = data.power;
    }

    public abstract void Die();

}
