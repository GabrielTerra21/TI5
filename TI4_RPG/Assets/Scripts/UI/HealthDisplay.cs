using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthDisplay : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text healthText;
    public float maxHealth;


    public virtual void SetValues(Character agent){
        maxHealth = agent.data.maxHp;

        healthBar.fillAmount = agent.life/maxHealth;
        healthText.text = $"{agent.life} / {maxHealth}";
    }

    public virtual void UpdateValues(float currentHp){
        healthBar.fillAmount = currentHp/maxHealth;
        healthText.text = $"{currentHp} / {maxHealth}";
    }
}
