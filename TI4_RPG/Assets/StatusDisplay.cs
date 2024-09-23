using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour {
    [SerializeField] private MyDictionary<string, Sprite> icons = new MyDictionary<string, Sprite>();
    private Image image;

    public enum statusEffect {
        ATTACKUP,
        ATTACKDOWN,
        DEFUP,
        DEFDOWN,
        NONE
    };
    public statusEffect currentStatus = statusEffect.NONE;
    

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            DisplayStatus(statusEffect.DEFDOWN);
        }
    }

    public void DisplayStatus(statusEffect status) {
        switch (status) {
            case statusEffect.ATTACKUP:
                image.sprite = icons["AttackUp"];
                image.color = Color.red;
                break;
            case statusEffect.ATTACKDOWN:
                image.sprite = icons["AttackDown"];
                image.color = Color.cyan;
                break;
            case statusEffect.DEFUP:
                image.sprite = icons["DefenseUp"];
                image.color = Color.green;
                break;
            case statusEffect.DEFDOWN:
                image.sprite = icons["DefenseDown"];
                image.color = Color.blue;
                break;
            case statusEffect.NONE:
                image.color = Color.clear;
                break;
            default:
                image.color = Color.clear;
                break;
        }
    }
}
