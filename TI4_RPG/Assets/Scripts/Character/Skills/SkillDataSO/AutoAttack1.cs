using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Skills/Attack", order = 2)]
public class AutoAttack1 : SkillDataSO {
    public float timeUntilHit;
    public override void OnCast(Character from, Character target) {
        //from.StartCoroutine(Attack(from, target));
        from.animator.SetTrigger("Attack");
        target.TakeDamage(Power);
    }

    IEnumerator Attack(Character from, Character target) {
        from.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(timeUntilHit);
        float timer = timeUntilHit;
        while (timeUntilHit > 0) {
            if (!GameManager.Instance.paused) timer -= Time.deltaTime;
            yield return null;
        }
        target.TakeDamage(Power);
    }
}
