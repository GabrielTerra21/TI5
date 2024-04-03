using System.Collections.Generic;
using UnityEngine;

public class EngageSphere : MonoBehaviour
{
    public string checkTag;
    public List<Character> inRange;


    public List<Character> GetEList() => inRange;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(checkTag))
        {
            inRange.Add(other.GetComponent<Character>());
            inRange.Sort();
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag(checkTag)) inRange.Remove(other.GetComponent<Character>());
    }
}
