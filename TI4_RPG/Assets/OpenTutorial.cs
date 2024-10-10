using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    public void ShowTutorial() {
        GameManager.Instance.OpenTutorial();
    }
}
