using UnityEngine;

public class TurnPostProcFX : MonoBehaviour{
    [SerializeField] private PixelationRenderFeature feature;

    public void Toggle(){
        feature.SetActive(!feature.isActive);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)) Toggle();
    }

    private void OnDisable(){
        feature.SetActive(false);
    }
}