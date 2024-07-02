using UnityEngine;
using UnityEngine.UI;

public class DisplaySkill : MonoBehaviour {
    [Header("Skill component")]
    public Skill skill;
    
    [Header("Components")]
    public Image image;
    
    
    private void Awake() {
        image = GetComponent<Image>();
    }
    
    private void FixedUpdate() {
        image.fillAmount =  (skill.data.CoolDown - skill.cdLeft)/skill.data.CoolDown;
    }
    
    public void UpdateSlot(Skill skill) {
        this.skill = skill;
        image.sprite = skill.data.Icon;
        image.type = Image.Type.Filled;
    }
}
