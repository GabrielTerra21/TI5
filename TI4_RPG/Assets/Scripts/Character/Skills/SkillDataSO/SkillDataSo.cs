using UnityEngine;

public abstract class SkillDataSO : ScriptableObject
{
    [Space(10)]
    [Header("SkillData Properties")]
    
    public float CoolDown, CastTime, Range;
    public bool outOfCombatCasting = false;
    public int Power;
    public Sprite Icon;
    public string SkillName ,Description;
    public GameObject Prefab;
    public int Price;
    public RuntimeAnimatorController animationOverride;
    public string AnimationID;
    
    public abstract void OnCast ( Character from, Character target);

    protected virtual void Rotate(Transform from, Vector3 target) {
        from.LookAt(target);
    }
}
