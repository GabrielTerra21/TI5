using UnityEngine.UI;
using UnityEngine;

public abstract class SkillDataSO : ScriptableObject
{
    [Space(10)]
    [Header("SkillData Properties")]
    
    public float CoolDown, CastTime, Range;
    public int Power;
    public Sprite Icon;
    public string SkillName ,Description;
    public GameObject Prefab;
    public RuntimeAnimatorController animationOverride;    
    
    public abstract void OnCast ( Character from, Character target);
}
