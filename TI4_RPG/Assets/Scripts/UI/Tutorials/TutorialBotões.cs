using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBotões : MonoBehaviour
{
    public GameObject buttonTutorial;
    void Start()
    {
        buttonTutorial.SetActive(false);
    }

    void Update()
    {
        if (buttonTutorial != null) {
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
