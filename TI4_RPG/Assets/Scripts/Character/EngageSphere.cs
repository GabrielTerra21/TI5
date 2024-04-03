using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EngageSphere : MonoBehaviour {
    public string checkTag;
    public List<Character> inRange;
    public UnityEvent OnEngage, OnDisengage;


    public List<Character> GetEList() => inRange;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(checkTag))
        {
            if(inRange.Count == 0) OnEngage.Invoke();
            inRange.Add(other.GetComponent<Character>());
            inRange.Sort();
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag(checkTag)) inRange.Remove(other.GetComponent<Character>());
        if (inRange.Count == 0) OnDisengage.Invoke();
    }
}
