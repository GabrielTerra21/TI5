using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Furia", order = 2)]
public class Furia : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        from.Furia(Power,CastTime);
    }
}
