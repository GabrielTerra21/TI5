using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [Header("Player info")]
    public int money = 300;
    
    [Header("Game Info")]
    public bool paused;
    public UnityEvent gameOver, pauseGame, unpauseGame;
    public Scene currentScene;
    public SkillDataSO empty;

    
    public void SpendMoney(int price) { money -= price; }
    public void GainMoney(int amount) { money += amount; }

    public void LoadNewScene(string sceneName) {
        pauseGame.RemoveAllListeners();
        unpauseGame.RemoveAllListeners();
        paused = false;
        currentScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.LoadScene("LoadingScreen");

    }
    public void LoadNewScene(Scene newScene) {
        pauseGame.RemoveAllListeners();
        unpauseGame.RemoveAllListeners();
        paused = false;
        currentScene = newScene;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void PauseGame() {
        pauseGame.Invoke();
        paused = true;
    }
}
