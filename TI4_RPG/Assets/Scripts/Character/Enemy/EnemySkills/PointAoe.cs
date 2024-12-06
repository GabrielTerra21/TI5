using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/CorujursoJumpAtt", order = 2)]
public class PointAoE : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        Corujurso c = from.GetComponent<Corujurso>();
        GameObject g = Instantiate(Prefab, c.jumpArea.position, from.transform.rotation, from.transform);
        AoE aoe = g.GetComponentInChildren<AoE>();
        from.dependencies.Add(g);
        aoe.CastAoE(Power + from.Power(), CastTime);
    }
}