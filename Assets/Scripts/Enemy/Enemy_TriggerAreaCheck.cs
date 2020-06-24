using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TriggerAreaCheck : MonoBehaviour
{
    private Enemy_Controller enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy_Controller>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.attackRange = true;
            enemy.lookArea.SetActive(true);
        }
    }
}
