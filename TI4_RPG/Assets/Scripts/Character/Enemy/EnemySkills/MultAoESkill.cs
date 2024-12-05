using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/MultEnemyAoE", order = 2)]
public class MultAoESkill : SkillDataSO
{
    public int quant;
    Vector3 pos;
    public override void OnCast(Character from, Character target)
    {
        for (int i = 0; i < quant; i++)
        {
            pos = new Vector3(Random.Range(from.transform.position.x - Range, from.transform.position.x + Range), 0, Random.Range(from.transform.position.z - Range, from.transform.position.z + Range));
            GameObject g = Instantiate(Prefab,pos,from.transform.rotation, from.transform);
            AoE aoe = g.GetComponentInChildren<AoE>();
            from.dependencies.Add(g);
            aoe.CastAoE(Power + from.Power(), CastTime);
        }
    }
}