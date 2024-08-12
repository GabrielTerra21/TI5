
using UnityEngine;

public class AttackMenu : MonoBehaviour {
    public AttackButton[] buttons;
    public AttackButton selected;
    [SerializeField] private CombatState player; 

    
    // Garante referência a todos os botões de ataque contidos no menu
    // Coisa a skill selecionada como skill "vazia"
    public void Start() {
        buttons = GetComponentsInChildren<AttackButton>();
        player = FindObjectOfType<CombatState>();
    }

    public void OpenMenu() {
        // Parar o tempo
        foreach (var data in buttons) { data.SetActive(); }
    }

    public void CloseMenu() {
        selected.selected = false;
        selected = null;
        foreach (var data in buttons) {
            data.SetInactive();
        }
        // Retornar tempo ao normal
    }
    
    // Checa qual botão foi selecionado,
    // chama o metodo para desabilitar a interatividade dos botões
    // e chama a execução da skill selecionada.
    public void OnSelection(AttackButton button) {
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
        // Retornar TimeScale ao normal
    }
}
