using System.Collections;
using UnityEngine;

public class AoE : MonoBehaviour
{
    public AoESO aoe;
    float count = 0;
    int power;
    public void CastAoE(int power, float castTime)
    {
        this.power = power;
        count = castTime;
    }
    private void Update()
    {
        if(!GameManager.Instance.paused && count > 0)
        {
            count -= Time.deltaTime;
        }
        else if(!GameManager.Instance.paused && count < 0)
        {
            aoe.DealDamage(transform.position, power);
            if (aoe.prefab != null)
            {
                Instantiate(aoe.prefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
