using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Range(1, 20)] [SerializeField] private float speed;
    [Range(5, 30)] [SerializeField] private float damage;
    private void Update()
    {
        Fireball_Attack();
    }
    public void Fireball_Attack()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject, 5f);
        }
    }
}
