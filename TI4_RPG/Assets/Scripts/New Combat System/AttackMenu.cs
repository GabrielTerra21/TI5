using System;
using UnityEngine;

public class AttackMenu : MonoBehaviour {
    public AttackButton[] buttons;
    public AttackButton selected;
    public bool operating;

    
    // Garante referência a todos os botões de ataque contidos no menu
    // Coisa a skill selecionada como skill "vazia"
    public void Start() {
        buttons = GetComponentsInChildren<AttackButton>();
    }

    // Checa qual botão foi selecionado,
    // chama o metodo para desabilitar a interatividade dos botões
    // e chama a execução da skill selecionada.
    public void OnSelection(AttackButton button) {
        if (operating) throw new Exception("On selection method was called twice on the attack menu");
        operating = true;
        
        foreach (var data in buttons) {
            if (data.selected) {
                selected = data;
            }
            data.SetInactive();
        }

        operating = false;
    }
}
