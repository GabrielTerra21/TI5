using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectArea : MonoBehaviour
{
    public Effect effect;
    float count = 0, countLoop = 0;
    List<Character> characters;
    private void Start()
    {
        count = effect.duration;
    }
    void Update()
    {
        if (!GameManager.Instance.paused && count > 0)
        {
            count -= Time.deltaTime;
            countLoop += Time.deltaTime;
            if (countLoop >= effect.interval)
            {
                countLoop = 0;
                effect.DoStuff(characters);
            }
        }
        else if(!GameManager.Instance.paused && count < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Character>(out Character c);
        characters.Add(c);
    }
    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<Character>(out Character c);
        characters.Remove(c);
    }
}
