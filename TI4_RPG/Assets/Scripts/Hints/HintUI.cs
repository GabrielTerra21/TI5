using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public TMP_Text title;
    public Image img;
    public Hint hint;
    public Sprite unlockedSprite;
    private void Start()
    {
        title.text = hint.title; 
    }
    public void BuyHint()
    {
        if (GameManager.Instance.money >= 50 && hint.blocked)
        {
            GameManager.Instance.SpendMoney(50);
            GameManager.Instance.UIUpdate();
            hint.blocked = false;
            img.sprite = unlockedSprite;
        }
    }
}
