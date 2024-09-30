using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEnemy : MonoBehaviour
{
    public Room room;
    public Effect effect,deatheffect;
    float countLoop = 0;
    public GameObject particles;
    public enum Type {Continuos, Instant}
    public Type type;

    private void Start()
    {
        if(type == Type.Instant)
        {
            foreach(Enemy enemy in room.enemies)
            {
                effect.DoStuff(enemy);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.paused && this.type == Type.Continuos)
        {
            countLoop += Time.fixedDeltaTime;
            if (countLoop >= effect.interval)
            {
                countLoop = 0;

                foreach (Character character in room.enemies)
                {
                    effect.DoStuff(character);
                }
                Instantiate(particles,transform.position,transform.rotation);
            }
        }
    }
    private void OnDestroy()
    {
        foreach (Character character in room.enemies)
        {
            deatheffect.DoStuff(character);
        }
    }
}
