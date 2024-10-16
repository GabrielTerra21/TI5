using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingItem : MonoBehaviour
{
    public SkillDataSO skill;
    public Image img;
    public Text title, description, price;
    void Start()
    {
        img.sprite = skill.Icon;
        title.text = skill.SkillName;
        description.text = skill.Description;
        price.text = "" + skill.Price;
    }
    public void Learn()
    {
        if(GameManager.Instance.ecos >= skill.Price)
        {
            Debug.Log("comprou");
            GameManager.Instance.SpendEcos(skill.Price);
            GameManager.Instance.UIUpdate();
            FindAnyObjectByType<SkillInventory>().Learn(skill);
            Destroy(gameObject);
        }
    }
}
