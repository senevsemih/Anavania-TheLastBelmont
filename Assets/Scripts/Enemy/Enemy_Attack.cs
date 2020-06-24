using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
