using UnityEngine;

public class ChestLocator : MonoBehaviour
{
    private void Start(){
        GetMaterial();   
    }

    private void GetMaterial(){
        if(LoadResourcesForDaniel.Instance != null){
            Debug.Log("Success");
            Renderer render = GetComponent<Renderer>();
            render.material = LoadResourcesForDaniel.Instance.GetMat();
        }
        else Debug.Log("Fail");
    }
}
