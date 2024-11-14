using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [SerializeField] public SkillDataSO skill;
    [SerializeField] private TMP_Text itemID,  priceTag, descriptor;
    [SerializeField] private Image sprite;
    [SerializeField] private int price;
    [SerializeField] private bool bought = false;
    [SerializeField] private GameObject darkened;
    public Action  purchased;
    
    private Vector3 defaultSize;
    [SerializeField] private Vector3 growthMod = new Vector3(.1f, .1f, 0);
    [SerializeField] private Vector3 shrinkMod = new Vector3(.2f, .2f, 0);

    protected void Start() {
        rect = GetComponent<RectTransform>();
        defaultSize = rect.localScale;
    }

    private void OnEnable() {
        if (!bought) SetUp();
        else SetInactive();
    }

    private void OnDisable() {
        purchased = null;
    }

    private void Purchase() {
        if (GameManager.Instance.ecos >= price) {
            GameManager.Instance.SpendEcos(price);
            GameManager.Instance.LearnSkill(skill);
            bought = true;
            purchased?.Invoke();
            SetInactive();
        }
        else {
            Debug.Log("O jogador não possui dinheiro suficiente para efetuar a compra.");
        }
    }

    private void OnValidate() {
        SetUp();
    }

    // Faz o setup inicial de colocar o sprite adequado, nome e preço da skill
    private void SetUp() {
        itemID.text = skill.SkillName;
        priceTag.text = price.ToString();
        sprite.sprite = skill.Icon;
        darkened.SetActive(false);
    }

    // Desabilita o display do sprite, apaga o texto e a descrição do item e liga
    // o overlay escuro para mostrar que o botão está inativo.
    private void SetInactive() {
        sprite.enabled = false;
        itemID.text = null;
        priceTag.text = null;
        darkened.SetActive(true);
        rect.localScale = defaultSize;
    }

    public void OnClick() {
        if (bought) return;
        
        Purchase();
        
    }
    
    // Aumenta o tamanho do botão ao botar o mouse sobre o botão
    public void OnHover() {
        if (bought) return;
        
        StartCoroutine(ChangeSize(defaultSize + growthMod));
        descriptor.text = skill.Description;
    }

    // Diminui o tamanho do botão depois de se retirar o mouse do botão
    public void OnExitHover() {
        if (bought) return;
        
        StartCoroutine(ChangeSize(defaultSize));
        descriptor.text = null;
    }
    
    // Interpola o tamanho do botão até o valor informado como argumento.
    IEnumerator ChangeSize(Vector3 targetSize) {
        Vector3 initialSize = rect.localScale;
        float duration = 0.05f;
        float timer = 0;
        while (timer < 1) {
            rect.localScale = Vector3.Slerp(initialSize, targetSize, timer);
            timer += Time.unscaledDeltaTime / duration;

            yield return null;
        }
        rect.localScale = targetSize;
    }
}
