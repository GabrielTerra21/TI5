using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitleScreen : MonoBehaviour
{
   public void ReturnToTitle() {
      SceneManager.LoadScene("MainMenu");
      Destroy(GameManager.Instance.gameObject);
   }
}
