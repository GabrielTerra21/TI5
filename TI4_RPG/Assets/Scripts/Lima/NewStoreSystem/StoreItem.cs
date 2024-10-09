using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [SerializeField] private SkillDataSO skill;
    [SerializeField] private TMP_Text itemID, itemDescription, priceTag;
    [SerializeField] private Image sprite;
    [SerializeField] private int price;
    [SerializeField] private bool boougth = false;
    [SerializeField] private GameObject highlight, darkened;
    
    private Vector3 defaultSize;
    [SerializeField] private Vector3 growthMod = new Vector3(.1f, .1f, 0);
    [SerializeField] private Vector3 shrinkMod = new Vector3(.2f, .2f, 0);

    protected void Start() {
        rect = GetComponent<RectTransform>();
        itemID.text = skill.SkillName;
        itemDescription.text = skill.Description;
        priceTag.text = price.ToString();
        sprite.sprite = skill.Icon;

        defaultSize = rect.localScale;
    }

    private void Purchase() {
        if (GameManager.Instance.ecos >= price) {
            // Aprende Skill
            boougth = true;
            // Desativar
        }
        else {
            // Falha
        }
    }

    private void SetInactive() {
        sprite.enabled = false;
        itemID.text = null;
        itemDescription.text = null;
        priceTag.text = null;
        darkened.SetActive(true);
        
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
