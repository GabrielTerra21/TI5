using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/EnemyAoE", order = 2)]
public class AoESkill : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        GameObject g = Instantiate(Prefab, from.transform.position,from.transform.rotation, from.transform) ;
        AoE aoe = g.GetComponentInChildren<AoE>();
        from.dependencies.Add(g);
        aoe.CastAoE(Power + from.Power(), CastTime);
    }
}