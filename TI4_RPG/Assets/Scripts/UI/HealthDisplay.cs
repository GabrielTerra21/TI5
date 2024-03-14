using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthDisplay : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text healthText;
    public float maxHealth;


    public virtual void SetValues(Character agent){
        maxHealth = agent.data.MaxHp;

        healthBar.fillAmount = agent.curHp/maxHealth;
        healthText.text = $"{agent.curHp} / {maxHealth}";
    }

    public virtual void UpdateValues(float currentHp){
        healthBar.fillAmount = currentHp/maxHealth;
        healthText.text = $"{currentHp} / {maxHealth}";
    }
}
