using UnityEngine;

public abstract class AoESO : ScriptableObject
{
    public int power;
    public int apDamage;
    public Vector3 hitbox;
    public abstract void DealDamage(Vector3 center,int power);
}