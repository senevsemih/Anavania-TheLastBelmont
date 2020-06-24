using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LookAreaCheck : MonoBehaviour
{
    private Enemy_Controller enemy;
    private bool attackRange;
    private Animator anim;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy_Controller>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(attackRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("FireSkull_attack"))
        {
            enemy.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackRange = false;
            gameObject.SetActive(false);
            enemy.triggerArea.SetActive(true);
            enemy.attackRange = false;
            enemy.SelectTarget();
        }
    }
}
