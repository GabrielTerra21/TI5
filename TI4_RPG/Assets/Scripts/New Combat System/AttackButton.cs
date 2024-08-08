using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
    public bool selected = false;
    public bool enlarged = false;
    public bool shrunk = false;
    

    // Garante que o componente EventTrigger
    // do botão esteja devidamente referenciado.
    private void Start() {
        if (eventT == null) eventT = GetComponent<EventTrigger>();
    }
    
    // Chamado ao clicar no botão
    public void OnSelection() {
        selected = true;
        StartCoroutine(Flash(20));
    }

    // Aumenta o tamanho do botão ao botar o mouse sobre o botão
    public void OnHover() {
        rect.localScale += growthMod;
        enlarged = true;
    }

    // Diminui o tamanho do botão depois de se retirar o mouse do botão
    public void OnExitHover() {
        rect.localScale -= growthMod;
        enlarged = false;
    }

    // Reduz o tamnaho e altera a cor do botão
    // para indicar ao jogador que o botão se tornou inativo
    public void SetInactive() {
        eventT.enabled = false;
        darkened.SetActive(true);
        rect.localScale -= shrinkMod;
        shrunk = true;
    }

    // Retorna o botão para o estado ativo
    public void SetActive() {
        eventT.enabled = true;
        darkened.SetActive(false);
        rect.localScale += shrinkMod;
        shrunk = false;
    }

    // Reseta as configurações do botão
    public void Reset() {
        if (enlarged) { OnExitHover(); }
        if (shrunk) { SetActive(); }
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

    // Produz efeito de flash ao selecionar skill
    IEnumerator Flash(float cycles) {
        while (cycles > 0) {
            highlight.SetActive(!highlight.activeInHierarchy);
            cycles--;
            yield return new WaitForSeconds(0.05f);
        }
        highlight.SetActive(true);
    }
    
}
