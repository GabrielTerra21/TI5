using System.Collections;
using TMPro;
using UnityEngine;

public class TextHealthBar : HealthBar {
    public TMP_Text healthText;
    
    [SerializeField] private float shakeIntensity = 2;

    protected override void Start() {
        base.Start();
        owner.OnDamage.AddListener(() => Shake());
    }
    
    // Pega os valores iniciais que a barra deve mostrar, recebendo o personagem correspondente
    // como parametro.
    public override void SetValues(Character agent) {
        base.SetValues(agent);
        healthText.text = $"{agent.life} / {maxHealth}";
    }

    // Atualiza os valores mostrados pela barra.
    public override void UpdateValues() {
        base.UpdateValues();
        healthText.text = $"{owner.life} / {maxHealth}";
    }

    // Chama a corotina de tremer a barra 
    public void Shake() {
        StartCoroutine(ShakeUI());
    }

    
    // Calcula um vetor de direção randomica o adiciona à posição do elemento, multiplicado 
    // pela escalar intensity, para produzir o efeito de "tremer" da barra
    IEnumerator ShakeUI() {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 OPos = rect.position;
        float timer = 1;
        while (timer > 0) {
            Vector3 shake = Random.insideUnitCircle * shakeIntensity;
            rect.position = OPos + shake * timer;
            timer -= Time.fixedDeltaTime;
            yield return null;
        }
        rect.position = OPos;
    }
}
