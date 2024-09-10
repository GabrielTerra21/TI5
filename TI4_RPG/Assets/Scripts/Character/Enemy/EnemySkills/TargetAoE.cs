using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/TargetAoE", order = 2)]
public class TargetAoE : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        GameObject g = Instantiate(Prefab, target.transform.position, from.transform.rotation);
        AoE aoe = g.GetComponentInChildren<AoE>();
        aoe.CastAoE(Power, CastTime);
    }
}