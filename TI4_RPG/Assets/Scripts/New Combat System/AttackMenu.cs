using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AttackMenu : MonoBehaviour {
    
    [SerializeField] private AttackButton[] buttons = new AttackButton[6];
    [SerializeField] private AttackButton selected;
    [SerializeField] private RectTransform rightWing, leftWing;
    [SerializeField] private RectTransform offScreenPosR, onScreenPosR, offScreenPosL, onScreenPosL;
    //[SerializeField] private PlayerInput playerInput;
    private CombatState player;

    
    // Garante referência a todos os botões de ataque contidos no menu
    // Coisa a skill selecionada como skill "vazia"
    private void Awake() {
        player = FindObjectOfType<CombatState>();
    }

    private void OnEnable() {
        InputManager.Instance.actions["Action"].performed += OpenMenu;
    }

    private void OnDisable() {
        if(InputManager.Instance)InputManager.Instance.actions["Action"].performed -= OpenMenu;
    }
    
    private void Start() {
        // Registra um novo envento ao OnPointerClick de cada botão
        // que chama o metodo OnSelection do menu
        foreach (var data in buttons) {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnSelection(); });
            data.eventT.triggers.Add(entry);
        }

        // Coloca o menu fora da tela e os botões como inativos.
        foreach (var data in buttons) {
            data.SetInactive();
        }
        leftWing.position = offScreenPosL.position;
        rightWing.position = offScreenPosR.position;
    }

    // Chama os metodos necessarios para animar o surgimento da UI de combate
    // e coloca os botões de ataque em estado ativo.
    public void OpenMenu() { 
        // Parar o tempo
        GameManager.Instance.PauseGame(); 
        StartCoroutine(EnterScreen(leftWing, onScreenPosL.position));
        StartCoroutine(EnterScreen(rightWing, onScreenPosR.position));
        foreach (var data in buttons) {
            data.SetActive();
        }
    }

    // Metodo Para Chamar OpenMenu atraves do InputManager
    private void OpenMenu(InputAction.CallbackContext context) {
        if(context.performed) OpenMenu();
    }

    // Reseta variaveis que precisam ser resetadas ao fim do uso da interface
    // anima a UI saindo da tela e coloca os botões de ataque em estado inativo.
    public void CloseMenu() {
        if (selected) selected.selected = false; 
        selected = null; 
        foreach (var data in buttons) { 
            data.SetInactive();
        }

        StartCoroutine(EnterScreen(leftWing, offScreenPosL.position)); 
        StartCoroutine(EnterScreen(rightWing, offScreenPosR.position)); 
        // Retornar tempo ao normal
        GameManager.Instance.UnpauseGame();
        
    }

    // Metodo Para Chamar CloseMenu atraves do InputManager
    private void CloseMenu(InputAction.CallbackContext context) {
        if (context.performed) CloseMenu();
    }
    
    // Checa qual botão foi selecionado,
    // chama o metodo para desabilitar a interatividade dos botões
    // e chama a execução da skill selecionada.
    public void OnSelection() {
        foreach (var data in buttons) {
            if (data.selected) {
                selected = data;
            }
            else data.SetInactive();
        }
        // Gastar barra de ação.
        selected.skill.OnCast(player.agent, player.ReturnTarget());
        selected.SetInactive();
        selected.selected = false;
        selected = null;
        CloseMenu();
        // Retornar TimeScale ao normal
    }

    public void UpdateMenu() {
        for (int i = 0; i < buttons.Length; i++) {
            if (player.skills[i] != null) {
                buttons[i].gameObject.SetActive(true);
                buttons[i].UpdateButton(player.skills[i]);
            }
            else buttons[i].gameObject.SetActive(false);
        }
    }

    // Produz efeito de animação do menu de combate deslizar para dentro/fora da tela.
    IEnumerator EnterScreen(RectTransform move, Vector3 targetPos) {
        float duration = 0.5f;
        float value = 0;
        Vector3 origin = move.position;
        while (value < 1) {
            move.position = Vector3.Lerp(origin, targetPos, value);
            value += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        move.position = targetPos;
    }
}
