using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void End(string cena)
    {
        GameManager.Instance.EnterUI();
        SceneManager.LoadScene(cena);
        Destroy(GameManager.Instance.gameObject);
    }
}