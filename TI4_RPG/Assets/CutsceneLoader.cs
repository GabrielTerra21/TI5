using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void Start(){
        source = GameManager.Instance.audioSource;
        StartCoroutine(PlayCutscene());
    }
    IEnumerator PlayCutscene(){   
        yield return new WaitUntil(() => !GameManager.Instance.paused);
        GameManager.Instance.PauseGame();
        source.Pause();
        AsyncOperation op =  SceneManager.LoadSceneAsync("SecondScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => op.isDone);
        yield return new WaitUntil(() => !GameManager.Instance.inCutscene);
        source.UnPause();
        GameManager.Instance.UnpauseGame();
    }
}
