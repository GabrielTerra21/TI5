
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/SpawnArea", order = 2)]
public class SpawnArea : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        GameObject g = Instantiate(Prefab, from.transform.position, from.transform.rotation);
    }
}
