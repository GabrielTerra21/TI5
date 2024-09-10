using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject 
{
    public int interval;
    public int duration;
    public int power;
    public abstract void DoStuff(List<Character> characters);
}
