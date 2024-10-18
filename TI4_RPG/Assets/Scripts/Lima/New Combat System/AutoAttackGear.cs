using UnityEngine;
using UnityEngine.UI;

public class AutoAttackGear : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private int speedID, fillAmountID, highlightID;
    private bool charged;

    private void Start() {
        image = GetComponent<Image>();
        speedID = Shader.PropertyToID("_Speed");
        fillAmountID = Shader.PropertyToID("_FillAmount");
        highlightID = Shader.PropertyToID("_UseHighlight");
    }

    public void UpdateGear(float fillAmount) {
        Debug.Log($"fillAmount = {fillAmount}");
        image.materialForRendering.SetFloat(fillAmountID,  fillAmount);
        if (fillAmount >= 1 ) {
            image.materialForRendering.SetFloat(speedID, 0);
            image.materialForRendering.SetFloat(highlightID, 1);
            charged = true;
        }
        else{
            image.materialForRendering.SetFloat(speedID, 37);
            image.materialForRendering.SetFloat(highlightID, 0);
            charged = false;
        }
    }
    
}
