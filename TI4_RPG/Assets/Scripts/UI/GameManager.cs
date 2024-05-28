using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public int money = 300;
    public SkillDataSO empty;
    public Text dinheiro;

    public void Compra(int i)
    {
        money -= i;
        dinheiro.text = "Dinheiro:" + money;
    }
    public void GanhaDinheiro(int i)
    {
        money += i;
        dinheiro.text = "Dinheiro:" + money;
    }
}
