using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
