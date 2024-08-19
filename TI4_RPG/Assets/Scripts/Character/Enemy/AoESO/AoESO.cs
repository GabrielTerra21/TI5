using UnityEngine;

public abstract class AoESO : ScriptableObject
{
    public int power;
    public abstract void DealDamage(Vector3 center,int power);
}

