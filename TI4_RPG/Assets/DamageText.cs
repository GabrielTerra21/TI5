using System.Collections;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour {
    private TMP_Text textComponent;
    [SerializeField] private int damageTook;
    [SerializeField] private GameObject icon;
    [SerializeField] private float maxTime = 2, timer = 0;
    [SerializeField] private Color weak = Color.gray, strong = Color.red;
    private bool running = false;

    private void Start() {
        textComponent = GetComponent<TMP_Text>();
    }

    public void DisplayDamage(int dmg) {
        timer = 0;
        damageTook += dmg;
        textComponent.text = damageTook.ToString();
        textComponent.color = Color.Lerp(weak, strong, Mathf.Abs((float)damageTook) / 50f);
        if (running == false) {
            StartCoroutine(Display());
        }
    }

    IEnumerator Display() {
        running = true;
        textComponent.enabled = true;
        //icon.SetActive(true);
        while (timer < maxTime) {
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        textComponent.enabled = false;
        //icon.SetActive(false);
        running = false;
        damageTook = 0;
    }
}
