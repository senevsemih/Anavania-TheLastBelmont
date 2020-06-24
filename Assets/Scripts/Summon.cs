using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject creditBoxTrigger;

    private Enemy_Health enemyHealth;
    private Enemy_Controller enemyController;
    private Animator anim;

    public bool summoning = false;
    [Space]
    [Header("Summon")]
    public GameObject demon;
    void Awake()
    {
        enemyHealth = GetComponent<Enemy_Health>();
        enemyController = GetComponent<Enemy_Controller>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyHealth.health <= 50 && summoning == false)
            StartCoroutine(ForceMonster());
        if (enemyHealth.health <= 0)
            creditBoxTrigger.SetActive(true);
    }
    IEnumerator ForceMonster()
    {
        anim.SetTrigger("Summoning");
        enemyController.enabled = false;

        yield return new WaitForSeconds(3);

        anim.SetBool("move", true);
        enemyController.enabled = true;
        summoning = true;
    }
    public void SummonMonster()
    {
        demon.SetActive(true);
    }
}
