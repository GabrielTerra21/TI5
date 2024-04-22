using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInterface : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] Slider slotSlider;
    private Image slotIcon;
    private SkillContainer slotContainer;
    [SerializeField]private Skill skill;

    public void Initialize(Character owner)
    {
        slotContainer = owner.GetComponent<SkillContainer>();
        if (slotContainer.skills[slotIndex] != null)
        {
            skill = slotContainer.skills[slotIndex];
            //slotIcon = skill.data.Icon;
            slotSlider.maxValue = skill.data.CoolDown;
        }
        else
        {
            //Destroy(gameObject);
        }
    }
    private void UpdateUI()
    {
        slotSlider.value = skill.cdLeft;
    }
    private void Update()
    {
        UpdateUI();
    }
    public void SetCast(int i)
    {
        SkillWheel.instance.SetCasting(i);
    }
}
