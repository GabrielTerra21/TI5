using TMPro;
using UnityEngine;

public class EnemyBar : HealthBar
{


    public void OnEnterCharacter(Character agent){
        owner = agent;
        SetValues(owner);
    }

    public void UpdateInfo(){
        UpdateValues();
        if(owner.life <= 0){
            
        }
    }

    public void Sleep(){
        owner = null;
        gameObject.SetActive(false);
    }
}
