using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [FormerlySerializedAs("Owner")]
    [Space(10)]
    [Header("Display Information")]
    public Character owner;
    
    [FormerlySerializedAs("healthBar")]
    [Space(10)]
    [Header("Components")]
    public Image barFill;
    public float maxHealth;


    private void Start() {
        owner.OnDamage.AddListener(() => UpdateValues());
        owner.OnHeal.AddListener(() => UpdateValues());
        SetValues(owner);
    }
    
    public virtual void SetValues(Character agent) {
        owner = agent;
        maxHealth = agent.data.maxHp;
        barFill.fillAmount = agent.life/maxHealth;
    }

    public virtual void UpdateValues() => barFill.fillAmount = owner.life/maxHealth;
}
