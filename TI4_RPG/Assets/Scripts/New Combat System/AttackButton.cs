using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {
    [Space(10)] [Header("UI Components")]
    public GameObject buttonInterface;
    public Image icon;
    public TMP_Text skillName;
    public TMP_Text description;
    public RectTransform rect;
    public GameObject highlight;
    public GameObject darkened;

    [Space(10)] [Header("Functional Components")]
    public SkillDataSO skill;
    public EventTrigger eventT;
    public Vector3 growthMod = new Vector3(.1f, .1f, 0);
    public Vector3 shrinkMod = new Vector3(.2f, .2f, 0);
    private Vector3 defaultSize;
    public bool selected = false;

    // Garante que o componente EventTrigger
    // do botão esteja devidamente referenciado.
    private void Start() {
        if (eventT == null) eventT = GetComponent<EventTrigger>();
        defaultSize = rect.localScale;
    }
    
    // Chamado ao clicar no botão
    [ExecuteInEditMode]
    public void OnSelection(BaseEventData context) {
        StartCoroutine(Flash(20));
    }

    // Aumenta o tamanho do botão ao botar o mouse sobre o botão
    [ExecuteInEditMode]
    public void OnHover() {
        //rect.localScale += growthMod;
        StartCoroutine(ChangeSize(defaultSize + growthMod));
    }

    // Diminui o tamanho do botão depois de se retirar o mouse do botão
    [ExecuteInEditMode]
    public void OnExitHover() {
        StartCoroutine(ChangeSize(defaultSize));
    }

    // Reduz o tamnaho e altera a cor do botão
    // para indicar ao jogador que o botão se tornou inativo
    public void SetInactive() {
        eventT.enabled = false;
        darkened.SetActive(true);
        StartCoroutine(ChangeSize(defaultSize - shrinkMod));
    }

    // Retorna o botão para o estado ativo
    public void SetActive() {
        eventT.enabled = true;
        darkened.SetActive(false);
        StartCoroutine(ChangeSize(defaultSize));
    }

    // Reseta as configurações do botão
    public void Reset() {
        StartCoroutine(ChangeSize(defaultSize));
        if(highlight.activeInHierarchy) highlight.SetActive(false);
    }

    // Atualiza os elementos de interface do botão
    public void UpdateButton(SkillDataSO setSkill) {
        skill = setSkill;
        if (skill == GameManager.Instance.empty) {
            buttonInterface.SetActive(false);
            return;
        }
        if (!buttonInterface.activeInHierarchy) buttonInterface.SetActive(true); 
        icon.sprite = skill.Icon;
        skillName.text = skill.SkillName;
        description.text = skill.Description;
    }

    // Interpola o tamanho do botão até o valor informado como argumento.
    IEnumerator ChangeSize(Vector3 targetSize) {
        Vector3 initialSize = rect.localScale;
        float duration = 0.05f;
        float timer = 0;
        while (timer < 1) {
            rect.localScale = Vector3.Slerp(initialSize, targetSize, timer);
            timer += Time.deltaTime / duration;

            yield return null;
        }
        rect.localScale = targetSize;
    }
    
    // Produz efeito de flash ao selecionar skill
    IEnumerator Flash(float cycles) {
        if (selected) throw new Exception("Flash Está sendo chamado multiplas vezes.");
        selected = true;

        eventT.enabled = false;
        while (cycles > 0) {
            highlight.SetActive(!highlight.activeInHierarchy);
            cycles--;
            yield return new WaitForSeconds(0.05f);
        }
        highlight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        highlight.SetActive(false);
        SetInactive();
    }
    
}
