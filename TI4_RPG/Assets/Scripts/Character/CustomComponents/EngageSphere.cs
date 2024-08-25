using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EngageSphere : MonoBehaviour {
    [SerializeField] private string checkTag;
    public List<Character> inRange;
    public UnityEvent OnEngage, OnDisengage;


    private void Awake() {
        inRange = new List<Character>(20);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(checkTag))
        {
            GetEnemy(other.GetComponent<Character>());
            if(inRange.Count == 1) OnEngage.Invoke();
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag(checkTag)){
            other.GetComponent<Character>().OnDeath.RemoveAllListeners();
            RemoveFromList(other.GetComponent<Character>());
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Vector4(1, 0, 0, .05f);
        SphereCollider col = GetComponent<SphereCollider>();
        Gizmos.DrawSphere(transform.position, col.radius);
    }

    private void GetEnemy(Character enemy){
        inRange.Add(enemy);
        enemy.OnDeath.AddListener(() => RemoveFromList(enemy));
        //inRange.Sort();
    }

    public void RemoveFromList(Character enemy) {
        inRange.Remove(enemy);
        if(inRange.Count == 0) OnDisengage.Invoke();
    }

    public Character GetNextTarget(Character current = null) {
        Character nTarget;
        if (current == null) {
            try { nTarget = inRange[0]; }
            catch { nTarget = null; }
        }
        else {
            int i = inRange.IndexOf(current);
            nTarget = inRange[(i + 1) % inRange.Count];
        }

        return nTarget;
    }
    
}
