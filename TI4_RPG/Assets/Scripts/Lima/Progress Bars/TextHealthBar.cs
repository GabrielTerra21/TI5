using TMPro;

public class TextHealthBar : HealthBar {
    public TMP_Text healthText;
    
    public override void SetValues(Character agent) {
        base.SetValues(agent);
        healthText.text = $"{agent.life} / {maxHealth}";
    }

    public override void UpdateValues() {
        base.UpdateValues();
        healthText.text = $"{owner.life} / {maxHealth}";
    }
}
