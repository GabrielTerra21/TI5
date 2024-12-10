using System.Collections;
using UnityEngine;

public class AoE : MonoBehaviour
{
    public AoESO aoe;
    float count = 0;
    int power;
    public bool player = false;
    public GameObject areas;
    public Enemy enemy;
    public void CastAoE(int power, float castTime)
    {
        this.power = power;
        count = castTime;
    }
    private void FixedUpdate()
    {
        if(!GameManager.Instance.paused && count > 0)
        {
            count -= Time.fixedDeltaTime;
        }
        else if(!GameManager.Instance.paused && count < 0)
        {
            if (player)
            {
                if (this.power != 0)
                {
                    GameManager.Instance.player.TakeDamage(this.power);
                }
                GameManager.Instance.LoseAP(aoe.apDamage);
                player = false;
            }
            if (aoe.prefab != null)
            {
                GameObject g = Instantiate(aoe.prefab, transform.position, transform.rotation);
                enemy.dependencies.Add(g);
                if(aoe.prefabDuration > 0)
                {
                    Destroy(g, aoe.prefabDuration);
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = false;
        }
    }
    public void HitBox(bool isIN)
    {
        player = isIN;
    }
}
