using System.Collections;
using UnityEngine;

public class AoE : MonoBehaviour
{
    public AoESO aoe;
    public void CastAoE(int power, float castTime)
    {
        StartCoroutine(CountDown(power, castTime - 0.1f));
    }
    public IEnumerator CountDown(int power,float countTime)
    {
        yield return new WaitForSeconds(countTime);

        Debug.Log("foi");
        aoe.DealDamage(transform.position, power);
    }
}
