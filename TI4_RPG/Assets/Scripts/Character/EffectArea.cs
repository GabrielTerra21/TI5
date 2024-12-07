using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectArea : MonoBehaviour
{
    public Effect effect;
    float count = 0, countLoop = 0;
    List<Character> characters;
    private Player player;
    private void Awake()
    {
        characters = new List<Character>();
    }
    private void Start()
    {
        count = effect.duration;
    }
    void FixedUpdate()
    {
        if (!GameManager.Instance.paused && count > 0)
        {
            count -= Time.fixedDeltaTime;
            countLoop += Time.fixedDeltaTime;
            if (countLoop >= effect.interval)
            {
                countLoop = 0;
                if (characters != null)
                {
                    foreach (Character character in characters)
                    {
                        effect.DoStuff(character);
                    }
                }
            }
        }
        else if(!GameManager.Instance.paused && count < 0)
        {
            if(player != null)player.ShowDebuff(false, effect.ID);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character c))
        {       
            characters.Add(c);
            if (c.CompareTag("Player"))
            {
                player = c.GetComponent<Player>();
                player.ShowDebuff(true, effect.ID);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character c))
        {
            characters.Remove(c);
            if (c.CompareTag("Player"))
            {
                player.ShowDebuff(false, effect.ID);
                player = null;
            }
        }
    }
}
