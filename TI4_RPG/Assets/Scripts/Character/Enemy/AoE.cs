using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{
    public AoESO aoe;

    public void CastAoE(int power)
    {
        aoe.DealDamage(transform.position,power);
    }
}
