using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Attack", order = 2)]
public class AutoAttack1 : SkillDataSO {
    //public float timeUntilHit;
    
    
    public override void OnCast(Character from, Character target) {
        //from.StartCoroutine(Attack(from, target));
        if(from.animator != null) from.animator.SetTrigger("Attack");
        target.TakeDamage(Power);
        GameManager.Instance.GainAP(5);
    }

    /*
    IEnumerator Attack(Character from, Character target) {
        if(from.animator != null) from.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(timeUntilHit);
        float timer = timeUntilHit;
        while (timeUntilHit > 0) {
            if (!GameManager.Instance.paused) timer -= Time.deltaTime;
            yield return null;
        }
        target.TakeDamage(Power);
    }
    */
}
