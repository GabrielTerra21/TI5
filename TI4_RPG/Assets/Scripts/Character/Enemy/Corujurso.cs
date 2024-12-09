using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Corujurso : Enemy
{
    public Transform[] waypoints;
    public Transform jumpArea;
    public GameObject[] spears;
    int spearsCount = 0;
    public Vector3 jumpPos = new Vector3();
    private int dmgDealt;

    protected override void Awake()
    {
        jumpArea = waypoints[Random.Range(0, waypoints.Length)];
        base.Awake();
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Unpause()
    {
        base.Unpause();
    }
    protected override void Start()
    {
        base.Start();
        homePos = transform.position;
    }
    public override int TakeDamage(int dmg)
    {
        StartCoroutine(Flash());
        life -= 1;
        dmgDealt += dmg;
        if(dmgDealt > 30)
        {
            ShowSpear();
            dmgDealt = 0;
        }
        OnDamage.Invoke();
        if (life <= 0)
        {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);

        if (damageText != null) damageText.DisplayDamage(1);

        return dmg;
    }

    public override void Die()
    {
        foreach (GameObject g in dependencies)
        {
            Destroy(g);
        }
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
        GameManager.Instance.GainEcos(5);
    }
    public void SpearDestroyd()
    {
        StartCoroutine(Flash());
        life -= 50;
        OnDamage.Invoke();
        if (life <= 0)
        {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);

        if (damageText != null) damageText.DisplayDamage(50);
    }
    public void ShowSpear()
    {
        if(spearsCount < spears.Length) spears[spearsCount].SetActive(true); spearsCount++;
        GameManager.Instance.player.GetComponent<CombatState>().FindEnemys();
    }
    IEnumerator Flash()
    {
        foreach (var data in renders)
        {
            data.material = highlightMat;
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var data in renders)
        {
            data.material = defaultMat;
        }
    }
    public void Jump()
    {
        transform.position = jumpPos;
    }
    public void Fall()
    {
        transform.position = jumpArea.position;
        jumpArea = waypoints[Random.Range(0, waypoints.Length)];
    }
}
