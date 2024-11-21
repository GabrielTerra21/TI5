using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Player info")] 
    public Player player;
    [FormerlySerializedAs("money")] public int ecos;

    [SerializeField] public int keys { get; private set; } = 0;
    public MyStat ap = new MyStat(25);
    private Exploring exploring;
    [SerializeField] private CombatState combatState;

    [Header("Game Info")] 
    public GameState state;
    public enum GameState {COMBAT, EXPLORATION, CINEMATIC}
    public bool paused;
    public UnityEvent pauseGame, unpauseGame, enterCombat, enterExploration, enterCinematic; 
    public string currentScene;
    public Portal previousDoor;
    public List<string> clearedRooms = new List<string>();
    [SerializeField] private string prevActionMap;
    public bool tutorial1 = false;

    [Header("UI Components")] 
    [SerializeField] private TMP_Text ecosText;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private StatusDisplay[] statusSlots;
    [SerializeField] private AutoAttackGear gearIcon;
    public TextHealthBar healthBar;
    public ActionBar actionBar;
    public SkillDataSO empty;
    public Vinhette vinhette;
    public Action UpdateUI;
    
    [Header("Managers Components")]
    public PlayerInput playerInput;
    public DialogueBox DialogueManager;
    
    public static GameManager Instance;

    // Codigo de Singleton.
    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    
    // Atribui o ID da cena atual ao valor currentScene, determina o stado do GameManager como
    // estado de exploração e faz update da UI.
    private void Start() {
        //playerInput = GetComponent<PlayerInput>();
        //playerInput = FindObjectOfType<PlayerInput>();
        //exploring = FindObjectOfType<Exploring>();
        //combatState = FindObjectOfType<CombatState>();
        //if(exploring != null) exploring.OnSubscribe();
        //if(combatState != null) combatState.OnSubscribe();
        currentScene = SceneManager.GetActiveScene().name;
        state = GameState.EXPLORATION;
        actionBar.UpdateBar(ap);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        keysText.text = " : " + keys/10 + keys % 10;
        ecosText.text = " : " + ecos/10 + ecos % 10;
    }

    // Reduz price da quantidade de dinheiro que o jogador tem.
    public void SpendEcos(int price) {
        ecos -= price;
        ecosText.text = " : " + ecos/10 + ecos % 10;
    }

    // Adiciona amount à quantidade de dinheiro do Jogador
    public void GainEcos(int amount) {
        ecos += amount;
        ecosText.text = " : " + ecos/10 + ecos % 10;
    }

    // Adiciona amount à barra de AP do jogador
    public void GainAP(int amount) {
        ap.currentValue += amount;
        actionBar.UpdateBar(ap);
    }

    // Reduz amount pontos da barra de AP do jogador
    public void LoseAP(int amount) {
        ap.currentValue -= amount;
        actionBar.UpdateBar(ap);
    }

    // Zera a barra de AP
    public void SpendAP() {
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
    }

    public void GainKey() {
        keys++;
        keysText.text = " : " + keys/10 + keys % 10;
    }

    public void LoseKey() {
        keys--;
        keysText.text = " : " + keys/10 + keys % 10 ;
    }

    public StatusDisplay GetStatusSlot() {
        for (int i = 0; i < statusSlots.Length; i++) {
            if (statusSlots[i].currentStatus == StatusDisplay.statusEffect.NONE) return statusSlots[i];
        }
        return null;
    }
    
    // Checa se o ID da sala informado esta presente na lista de salas completas e retorna o resultado.
    public bool CheckClearedRooms(string ID) {
        return clearedRooms.Contains(ID);
    }

    
    // Adiciona ID à lista de salas vencidas, a menos que a ID informada ja se encontre na lista.
    public void AddClearedRoom(string ID) {
        if(clearedRooms.Contains(ID))return;
        else clearedRooms.Add(ID);
    }

    // Pausa o jogo, chama corotiona para carregar a cena anterior e atualiza as UI's
    // chamado pelo jogador quando ele morre.
    public void DeathLoad() {
        PauseGame();
        StartCoroutine(LoadPreviousScreen());
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
        healthBar.UpdateValues();
    }

    
    // Cuida das animações da vinheta de morte, teleporta o jogador de volta para a sala anterior
    // e lida com o carregamento e descarregamento de cenas.
    IEnumerator LoadPreviousScreen() {
        yield return new WaitForSeconds(vinhette.Cover());
        
        AsyncOperation sceneToLoad = SceneManager.LoadSceneAsync(previousDoor.roomID, LoadSceneMode.Additive);
        AsyncOperation sceneToUnload = SceneManager.UnloadSceneAsync(currentScene);
        
        yield return new WaitUntil(() => sceneToLoad.isDone);
        
        player.Teleport(previousDoor.spawnPoint.position);
        
        yield return new WaitUntil(() => sceneToUnload.isDone);
        yield return new WaitForSeconds(vinhette.Uncover());
        
        yield return new WaitForSeconds(1);
        
        Instance.UnpauseGame();
    }

    // Pausa o jogo e chama o evento de pausa
    public void PauseGame() {
        if (paused)  throw new Exception("Game is already paused"); 
        if (currentScene == "Menu") return;
        pauseGame.Invoke();
        paused = true;
    }

    // Chama a entrada do estado de combate
    public void CallCombatMode() {
        ap.currentValue = 0;
        actionBar.UpdateBar(ap);
        state = GameState.COMBAT;
        StartCoroutine(EventBuffer(enterCombat));
    }

    // Chama a entrada do estado de exploração
    public void CallExploration() {
        ap.currentValue = 0;
        state = GameState.EXPLORATION;
        StartCoroutine(EventBuffer(enterExploration));
    }

    public void CallCinematic() {
        state = GameState.CINEMATIC;
        enterCinematic.Invoke();
    }

    // Pausa o jogo e troca o esquema de controles para os controles de UI.
    public void EnterUI() {
        PauseGame();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        prevActionMap = playerInput.currentActionMap.name;
        playerInput.SwitchCurrentActionMap("MyUI");
    }

    // Despausa o jogo e retorna os controles para o modo de exploração.
    public void ExitUI() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UnpauseGame();
        playerInput.SwitchCurrentActionMap(prevActionMap);
    }

    public void LearnSkill(SkillDataSO skill) {
        combatState.LearnSkill(skill);
    }

    // Despausa o jogo e invoca o evento de despausa.
    public void UnpauseGame() {
        if (!paused) { throw new Exception("Is not paused"); }
        unpauseGame.Invoke();
        paused = false;
    }

    // Tenho QUASE certeza que não é utilizado
    public void TogglePause() {
        if (paused) UnpauseGame();
        else PauseGame();
    }

    // Tenho quase certeza que só é utilizado para atualizar a barra de vida
    // Poderia ser refatorado com toda certeza, dito isso, falta tempo...
    public void UIUpdate()
    {
        UpdateUI?.Invoke();
    }
    
    // Aguarda até que o jogo não esteja mais em estado de pausa para invocar o evento
    // necessario para que as animações de interface toquem
    IEnumerator EventBuffer(UnityEvent nEvent) {
        yield return new WaitUntil(() => !paused);
        nEvent.Invoke();
    }
}
