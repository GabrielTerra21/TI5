using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    public Image profilePic;
    public TMP_Text nameSpace, text;
    private int maxCharCount;
    public bool ready;
    public Queue<Dialogue> dialogue;
    
    public void Paste(Dialogue content) {
        GameManager.Instance.playerInput.actions["Confirm"].performed += Skip; 
        text.text = null;
        profilePic.sprite = content.owner.data.profile;
        StartCoroutine(StepPasting(content.text));
    }
    
    public void Skip(InputAction.CallbackContext context) {
        Dialogue next = dialogue.Dequeue();
        if(next == null) EndDialogue();
        else Paste(next);
    }

    public void EndDialogue() {
        GameManager.Instance.playerInput.actions["Confirm"].performed -= Skip;
    }

    IEnumerator StepPasting(string content) {
        foreach (char value in content) {
            text.text += value;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
