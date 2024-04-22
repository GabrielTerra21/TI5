using UnityEngine.UI;
using UnityEngine;

public abstract class SkillDataSo : ScriptableObject 
{
    public float CoolDown, CastTime, Range;
    public int Power;
    public string Description, SkillName;
    public Image Icon;
    public GameObject Prefab;
    public RuntimeAnimatorController animationOverride;    
    
    public abstract void OnCast ( Character from, Character target);
}
