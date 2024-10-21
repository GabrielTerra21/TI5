using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Animator chestAnimator;

    private enum REWARD {
        MONEY,
        KEY,
        SKILL
    }
    [SerializeField] private REWARD type;
    [SerializeField] private int amount;
    [SerializeField] private SkillDataSO skill;
    [SerializeField] bool isUsed = false;
    [SerializeField] private GameObject moneyAnim, keyAnim, skillAnim;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private float dispawn;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        if (!isUsed)
        {
            chestAnimator.SetTrigger("OpenChest");

	    audioSource.clip = audioClip;
	    audioSource.Play();

            switch (type) {
                case REWARD.MONEY:
                    GameManager.Instance.GainEcos(amount);
                    moneyText.text = "$" + amount/10 + amount % 10;
                    moneyAnim.SetActive(true);
                    Destroy(moneyAnim, dispawn);
                    break;
                case REWARD.KEY:
                    GameManager.Instance.GainKey();
                    keyAnim.SetActive(true);
                    break;
                case REWARD.SKILL:
                    GameManager.Instance.LearnSkill(skill);
                    skillAnim.GetComponent<Image>().sprite = skill.Icon;
                    skillAnim.SetActive(true);
                    Destroy(skillAnim, dispawn);
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
