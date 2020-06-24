using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public Animator animator;
    public int health = 100;

    public bool isInvulnerable = false;

    [Space]
    [Header("Take Damage Effect")]
    public Material matRed;
    public Material matBlack;
    private Material matDefault;

    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        
        StartCoroutine(DamageAnimation());

        if (health <= 0)
        {
            animator.SetTrigger("Dead");

            FindObjectOfType<AudioManager>().Play("Enemy Kill");
        }
    }
    IEnumerator DamageAnimation()
    {
        sr.material = matBlack;
        yield return new WaitForSeconds(.1f);

        sr.material = matRed;
        yield return new WaitForSeconds(.1f);

        sr.material = matDefault;
        yield return new WaitForSeconds(.1f);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
