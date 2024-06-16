using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject lojaMenu;

    public void AbreLoja()
    {
        lojaMenu.SetActive(!lojaMenu.activeInHierarchy);
    }
}
