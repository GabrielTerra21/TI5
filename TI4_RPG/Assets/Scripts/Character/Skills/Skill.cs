using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour {
    [Space(10)]
    [Header("SourceSkill")]
    public SkillDataSo data;
    
    [Space(10)]
    [Header("Skill info")]
    public float cdLeft; // Time until cool down ends and skill is ready to be used again 
    public float timeToCast; // Time until skill is casted upon activation
    public bool ready;


    private void Awake() {
        ready = true;
    }

    public void OnCast(Character owner, Character target) {
        if (owner.actionable && ready) {
            Debug.Log("here");
            StartCoroutine(Casting(owner, target));
        }
    }
    
    IEnumerator Casting( Character owner, Character target) {
        owner.actionable = false;
        float count = 0;

        while (count < data.CastTime) {
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
            cdLeft = Mathf.Lerp(data.CoolDown, 0, count / data.CoolDown);
            count += Time.deltaTime;
            yield return null;
        }

        ready = true;
    } 
}
