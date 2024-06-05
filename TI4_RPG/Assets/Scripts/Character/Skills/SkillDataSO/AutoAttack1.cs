using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Skills/Attack", order = 2)]
public class AutoAttack1 : SkillDataSO {
    public float timeUntilHit;
    public override void OnCast(Character from, Character target) {
        from.StartCoroutine(Attack(from, target));
    }

    IEnumerator Attack(Character from, Character target) {
        from.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(timeUntilHit);
        target.TakeDamage(Power);
    }
}
