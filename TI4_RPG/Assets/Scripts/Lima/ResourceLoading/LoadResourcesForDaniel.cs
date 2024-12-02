using System.Collections;
using UnityEngine;

public class LoadResourcesForDaniel : MonoBehaviour
{
    [SerializeField] private Material chestMat;
    public static LoadResourcesForDaniel Instance;

    private void Awake(){
        if(Instance != null){
            Destroy(Instance);
            Instance = this;
        }
        else Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start(){
        StartCoroutine(WaitForRequest());
    }

    public Material GetMat() { return chestMat;}

    IEnumerator WaitForRequest(){
        int index = Random.Range(1, 4);
        ResourceRequest op = Resources.LoadAsync<Material>("ChestResource" + index);
        Debug.Log("Aguardando recurso");
        yield return new WaitUntil( () => op.isDone);
        Debug.Log("Recurso Carregado");
        chestMat = op.asset as Material;
        Debug.Log("Material Carregado : " + chestMat.name);
    }
}
