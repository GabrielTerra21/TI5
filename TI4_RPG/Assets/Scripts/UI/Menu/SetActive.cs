using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoAtivarDesativar : MonoBehaviour
{
    public List<GameObject> objetosParaAtivarDesativar;

    public void AtivarDesativarObjetos()
    {
        foreach(GameObject objeto in objetosParaAtivarDesativar)
        {
            objeto.SetActive(!objeto.activeSelf);
        }
    }
}