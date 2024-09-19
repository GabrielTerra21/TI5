using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public TMP_Text title;
    public Image img;
    public Hint hint;
    public Sprite unlockedSprite;
    public GameObject placa;
    private void Start()
    {
        hint.blocked = true;
        title.text = hint.title; 
    }
    public void BuyHint()
    {
        if (GameManager.Instance.ecos >= 50 && hint.blocked)
        {
            GameManager.Instance.SpendEcos(50);
            GameManager.Instance.UIUpdate();
            hint.blocked = false;
            img.sprite = unlockedSprite;
            placa.SetActive(true);
        }
    }
}
