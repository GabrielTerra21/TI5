using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour{
    [Header("Player info")] public int money = 300;

    [Header("Game Info")] public bool paused;
    public UnityEvent gameOver, pauseGame, unpauseGame;
    public string currentScene;
    public SkillDataSO empty;
    public PlayerInput playerInput;
    public static GameManager Instance;
    public enum GameState {COMBAT, EXPLORATION}
    public GameState state;
    public Action UpdateUI;
    public Player player;
    public SkillDataSO fire;

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
        state = GameState.EXPLORATION;
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

    public void PauseGame() {
        if (currentScene == "Menu") return;
        pauseGame.Invoke();
        paused = true;
    }

    public void EnterUI() {
        PauseGame();
        playerInput.SwitchCurrentActionMap("MyUI");
    }

    public void ExitUI() {
        UnpauseGame();
        playerInput.SwitchCurrentActionMap("Action");
    }

    public void UnpauseGame() {
        unpauseGame.Invoke();
        paused = false;
    }

    public void TogglePause() {
        if (paused) UnpauseGame();
        else PauseGame();
    }

    public void UIUpdate()
    {
        UpdateUI?.Invoke();
    }
    public void Cheat()
    {
        player.life = 999999;
        FindAnyObjectByType<SkillInventory>().Learn(fire);
        return;
    }
}
