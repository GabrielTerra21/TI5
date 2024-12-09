using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void End(string cena)
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(cena);
    }
}
