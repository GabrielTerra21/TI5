using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMove : MonoBehaviour
{
    //Define qual a cena atual
    public GameObject movementTutorialImage;

    void Start(){
        Scene currentScene = SceneManager.GetActiveScene();
        movementTutorialImage.SetActive(false);
        //Pega o nome da cena atual
        string sceneName = currentScene.name;
        //Se a cena atual for a primeira fase, fica ativo
        if(sceneName == "Fase1"){
            movementTutorialImage.SetActive(true);
        }
    }

    private void Update()
    {
        //Se apertar D, ou seja, andou na direção certa, desativa essa UI inteira, pro Update nn ficar comendo
        if (Input.GetKeyDown(KeyCode.D)){
            this.gameObject.SetActive(false);
        }
    }
}
