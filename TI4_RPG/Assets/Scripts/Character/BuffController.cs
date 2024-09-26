using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public List<BuffDebuff> mDebuffs;
    public static BuffController instance;
    private void Start()
    {
        mDebuffs = new List<BuffDebuff>();
        instance = this;
    }
    private void FixedUpdate()
    {
        for(int i = 0;i < mDebuffs.Count; i++)
        {
            mDebuffs[i].timer -= Time.fixedDeltaTime;
            if(mDebuffs[i].timer <= 0)
            {
                if(mDebuffs[i].tipo == "armor")
                {
                    mDebuffs[i].alvo.ApplyBonusDefense(-mDebuffs[i].power);
                }
                else
                {
                    mDebuffs[i].alvo.ApplyBonusAttack(-mDebuffs[i].power);
                }
                mDebuffs.Remove(mDebuffs[i]);   
            }
        }
    }
    public void ListarBuff(float timer,string tipo,Character alvo,int power)
    {
        mDebuffs.Add(new BuffDebuff(timer,tipo,alvo,power));
    }
}
