using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Skills/AutoAttack", order = 2)]
public class AutoAttack1 : SkillDataSo
{
    public override void OnCast(Character from, Character target) {
        Debug.Log("AutoAttack hit");
        from.animator.SetTrigger("Attack");
        target.TakeDamage(Power);
    }
}
