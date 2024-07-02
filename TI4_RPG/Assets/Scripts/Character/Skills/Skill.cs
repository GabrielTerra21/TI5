using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {
    [Space(10)]
    [Header("SourceSkill")]
    public SkillDataSO data;
    
    [Space(10)]
    [Header("Skill info")]
    public float cdLeft; // Time until cool down ends and skill is ready to be used again 
    public float timeToCast; // Time until skill is casted upon activation
    public bool ready;


    private void Awake() {
        ready = true;
    }

    public void OnCast(Character owner, Character target) {
        //Debug.Log($"Starting casting {data.SkillName} on {target.data.charName}");
        if (GameManager.Instance.state == GameManager.GameState.EXPLORATION && !data.outOfCombatCasting) return;
        if (data.Range != 0) {
            if (!InRange(owner.transform, target.transform, data.Range)) {
                Debug.Log("Casting failed due to range");
                return;
            }
            Debug.Log("Casting hasn't failed due to range");
        }  
        if (owner.actionable && ready) {
            Debug.Log($"Casting {data.SkillName}...");
            StartCoroutine(Casting(owner, target));
            //Debug.Log($"Casted {data.SkillName} on {target.data.charName}");
        }
    }
    //Passivel de mudan�a
    public void OutCombtCast()
    {

    }

    private bool InRange(Transform from, Transform to, float range) {
        Vector3 offset = to.position - from.position;
        float sqrLength = offset.sqrMagnitude;
        return sqrLength <= range * range;
    }
    
    IEnumerator Casting( Character owner, Character target) {
        owner.actionable = false;
        float count = 0;

        while (count < data.CastTime) {
            yield return new WaitWhile((() => GameManager.Instance.paused)); // Pauses Coroutine if game is paused;
            timeToCast = Mathf.Lerp(0, data.CastTime, count / data.CastTime);
            count += Time.deltaTime;
            yield return null;
        }
        data.OnCast(owner ,target);
        StartCoroutine(EnterCoolDown());
        
        owner.actionable = true;
    }
    
    IEnumerator EnterCoolDown() {
        ready = false;
        float count = 0;

        while (count < data.CoolDown) {
            yield return new WaitWhile((() => GameManager.Instance.paused));
            cdLeft = Mathf.Lerp(data.CoolDown, 0, count / data.CoolDown);
            count += Time.deltaTime;
            yield return null;
        }

        ready = true;
    } 
}
