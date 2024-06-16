using UnityEngine.SceneManagement;
using UnityEngine;

public class BtnControl : MonoBehaviour
{
    public string nomeCena;
    public void BtnMudaCena(){
        SceneManager.LoadScene(nomeCena);
    }

    public void BtnQuit(){
        Application.Quit();
        Debug.Log("Saiu!!");
    }
}
