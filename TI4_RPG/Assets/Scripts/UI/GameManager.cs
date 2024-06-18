using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    [Header("Player info")] public int money = 300;

    [Header("Game Info")] public bool paused;
    public UnityEvent gameOver, pauseGame, unpauseGame;
    public string currentScene;
    public SkillDataSO empty;
    public static GameManager Instance;
    public enum GameState {COMBAT, EXPLORATION, CINEMATIC}
    public GameState state;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void Start() {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void SpendMoney(int price) {
        money -= price;
    }

    public void GainMoney(int amount) {
        money += amount;
    }

    public void LoadNewScene(string sceneName) {
        pauseGame.RemoveAllListeners();
        unpauseGame.RemoveAllListeners();
        paused = false;
        currentScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    // public void LoadNewScene(Scene newScene) {
    //     pauseGame.RemoveAllListeners();
    //     unpauseGame.RemoveAllListeners();
    //     paused = false;
    //     currentScene = newScene;
    //     SceneManager.LoadScene("LoadingScreen");
    // }

    public void PauseGame() {
        if (currentScene == "Menu") return;
        Debug.Log("Pausing Game");
        pauseGame.Invoke();
        paused = true;
        Debug.Log("Game paused");
    }

    public void UnpauseGame() {
        unpauseGame.Invoke();
        paused = false;
    }

    public void TogglePause() {
        if (paused) UnpauseGame();
        else PauseGame();
    }
    
    IEnumerator Load() {
        Debug.Log($"loading {GameManager.Instance.currentScene}");
        AsyncOperation loading = SceneManager.LoadSceneAsync(GameManager.Instance.currentScene);
        while (!loading.isDone) {
            //bar.fillAmount = Mathf.Lerp(0f, 1f, loading.progress);
            yield return null;
        }
    }

}
