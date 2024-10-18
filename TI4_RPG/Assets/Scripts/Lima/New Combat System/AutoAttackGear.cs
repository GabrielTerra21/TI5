using UnityEngine;
using UnityEngine.UI;

public class AutoAttackGear : MonoBehaviour {
    [SerializeField] private CombatState player;
    [SerializeField] private float fillAmount;
    [SerializeField] private float skillCD;
    [SerializeField] private Image image;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<CombatState>();
        skillCD = player.autoAttack.CoolDown;
        image = GetComponent<Image>();
    }

    private void FixedUpdate() {
        fillAmount = 1 - player.coolDown / skillCD;
        image.materialForRendering.SetFloat("_FillAmount", fillAmount);
        if (fillAmount >= 1) {
            image.materialForRendering.SetFloat("_Speed", 0);
        }
        else {
            image.materialForRendering.SetFloat("_Speed", 37);
        }
    }
    
}
