using UnityEngine;

public class DialogueNPC : WaitingTrigger {
    public Dialogue[] dialogue;
    [SerializeField] private int counter = 0;

    
    // Adiciona ao evento action uma chamada ao DialogueManager para começar o dialogo
    // faz com que o jogador itere pelo arranjo de dialogos, até que chegue no ultimo dialogo do NPC
    // neste caso, o NPC irá repetir o ultimo dialogo nas futuras interações.
    protected override void Start() {
        action.AddListener(() => {
            GameManager.Instance.DialogueManager.StartDialogue(dialogue[counter]);
            if (counter < dialogue.Length - 1) counter++;
        });
    }
}
