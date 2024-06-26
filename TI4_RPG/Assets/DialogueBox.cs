using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    public GameObject dialogueScreen;
    public Image profilePic;
    public TMP_Text nameSpace, text;
    private int maxCharCount;
    public Queue<Speech> dialogue;
    
    
    public void Paste(Speech content) {
        text.text = null;
        nameSpace.text = content.owner.charName;
        profilePic.sprite = content.owner.profile;
        StartCoroutine(StepPasting(content.text));
    }
    
    public void Skip(InputAction.CallbackContext context) {
        StopAllCoroutines();
        if (dialogue.Count > 0)  Paste(dialogue.Dequeue()); 
        else EndDialogue();
    }
    
    // Este metodo deve ser usado unica e exclusivamente para a primeira execução de exibir o dialogo, ja que não é possivel invocar o metodo acima sem um Callback Context adequado
    private void Commence() { 
        StopAllCoroutines();
        if (dialogue.Count > 0)  Paste(dialogue.Dequeue()); 
        else EndDialogue();
    }

    public void EndDialogue() {
        GameManager.Instance.playerInput.actions["Confirm"].performed -= Skip;
        dialogueScreen.SetActive(false);
        GameManager.Instance.ExitUI();
    }

    public void StartDialogue(Dialogue source) {
        GameManager.Instance.EnterUI();
        dialogue = new Queue<Speech>(source.dialogue.Count);
        foreach (var data in source.dialogue) {
            dialogue.Enqueue(data);
        }
        dialogueScreen.SetActive(true);
        GameManager.Instance.playerInput.actions["Confirm"].performed += Skip; 
        Commence();
    }

    IEnumerator StepPasting(string content) {
        foreach (char value in content) {
            text.text += value;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
