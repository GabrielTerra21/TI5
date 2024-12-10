using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Furia", order = 2)]
public class Furia : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        from.ApplyBonusAttack(Power);
        from.StartCoroutine(Coldonw(from,CastTime));
        //BuffController.instance.ListarBuff(CastTime, "attack", from, Power);
    }
    IEnumerator Coldonw(Character from, float time)
    {
        yield return new WaitForSeconds(time);
        from.ApplyBonusAttack(-Power);
    }
}
