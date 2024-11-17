using UnityEngine;
using UnityEngine.UI;

public class AutoAttackGear : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private int  fillAmountID;
    private bool charged;

    private void Start() {
        image = GetComponent<Image>();
        fillAmountID = Shader.PropertyToID("_FillAmount");
    }

    public void UpdateGear(float fillAmount) {
        image.materialForRendering.SetFloat(fillAmountID,  fillAmount);
        if (fillAmount >= 1  && !charged) {
            charged = true;
        }
        else if (fillAmount < 1 && charged){
            charged = false;
        }
    }
    
}
