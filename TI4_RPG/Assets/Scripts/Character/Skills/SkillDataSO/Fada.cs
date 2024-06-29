using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Fada", order = 2)]
public class Fada : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        Vector3 pos = new Vector3(from.transform.position.x, from.transform.position.y + 0.5f, from.transform.position.z);
        GameObject turret = Instantiate(Prefab, pos, from.transform.rotation);
        Turret p = turret.GetComponent<Turret>();
        p.power = Power;
        p.from = from;
    }
}
