using UnityEngine;

public abstract class Effect : ScriptableObject 
{
    public float interval;
    public int duration;
    public int power;
    public int ID;

    public abstract void DoStuff(Character character);
}
