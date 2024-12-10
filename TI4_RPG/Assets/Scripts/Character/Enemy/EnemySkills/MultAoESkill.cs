using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/MultEnemyAoE", order = 2)]
public class MultAoESkill : SkillDataSO
{
    public int quant;
    Vector3 pos;
    public override void OnCast(Character from, Character target)
    {
        GameObject g = Instantiate(Prefab, target.transform.position, from.transform.rotation, from.transform);
        AoE aoe = g.GetComponentInChildren<AoE>();
        aoe.enemy = from.GetComponent<Enemy>();
        from.dependencies.Add(g);
        aoe.CastAoE(Power + from.Power(), CastTime);
        for (int i = 0; i < quant - 1; i++)
        {
            pos = new Vector3(Random.Range(from.transform.position.x - Range, from.transform.position.x + Range), from.transform.position.y, Random.Range(from.transform.position.z - Range, from.transform.position.z + Range));
            g = Instantiate(Prefab,pos,from.transform.rotation, from.transform);
            aoe = g.GetComponentInChildren<AoE>();
            from.dependencies.Add(g);
            aoe.enemy = from.GetComponent<Enemy>();
            aoe.CastAoE(Power + from.Power(), CastTime);
        }
    }
}