using UnityEngine.UI;
using UnityEngine;

public abstract class SkillDataSo : CollectableSO
{
    [Space(10)]
    [Header("SkillData Properties")]
    public float CoolDown, CastTime, Range;
    public int Power;
    public string Description;
    public GameObject Prefab;
    public RuntimeAnimatorController animationOverride;    
    
    public abstract void OnCast ( Character from, Character target);
}
