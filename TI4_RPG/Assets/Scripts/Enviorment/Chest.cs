using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator chestAnimator;

    private enum REWARD {
        MONEY,
        KEY
    }
    [SerializeField] private REWARD type;
    [SerializeField] private int amount;
    [SerializeField] bool isUsed = false;
    [SerializeField] private GameObject moneyAnim, keyAnim;
    [SerializeField] private TMP_Text moneyText;
    void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        if (!isUsed)
        {
            chestAnimator.SetTrigger("OpenChest");
            switch (type) {
                case REWARD.MONEY:
                    GameManager.Instance.GainEcos(amount);
                    moneyText.text = "$" + amount/10 + amount % 10;
                    moneyAnim.SetActive(true);
                    break;
                case REWARD.KEY:
                    GameManager.Instance.GainKey();
                    keyAnim.SetActive(true);
                    break;
            }
            /*
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("OpenChest");
            }
            moneyAnim.SetActive(true);
            */
            isUsed = true;
        }
    }
}
