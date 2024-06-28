using UnityEngine.SceneManagement;
using UnityEngine;

public class BtnControl : MonoBehaviour
{
    public void BtnMudaCena(string nomeCena){
        SceneManager.LoadScene(nomeCena);
    }

    public void BtnQuit(){
        Application.Quit();
        Debug.Log("Saiu!!");
    }
}
