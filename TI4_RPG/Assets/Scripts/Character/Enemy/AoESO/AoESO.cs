using UnityEngine;

public abstract class AoESO : ScriptableObject
{
    public int power;
    public GameObject prefab;
    public int apDamage;
    public Vector3 hitbox;
    public Collider col;
    public abstract void DealDamage(Vector3 center,int power);
}