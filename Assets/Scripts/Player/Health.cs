using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private SpriteRenderer srs;
    private Rigidbody2D rigidb2D;
    private Animator anim;
    private Movement movement;
    private Color colorDefault;

    public float hurtMove;
    public float hurtMoveUp;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

    public float MaxHealth => maxHealth;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

            OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
            {
                CurrentHealth = currentHealth,
                MaxHealth = maxHealth
            });
        }
    }
    private void Start()
    {
        srs = GetComponent<SpriteRenderer>();
        rigidb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();

        colorDefault = srs.color;
        CurrentHealth = maxHealth;
    }
    public void Add(float value)
    {
        value = Mathf.Max(value, 0f);

        CurrentHealth += value;
    }
    public void TakeDamage(float damage)
    {
        damage = Mathf.Max(damage, 0f);

        CurrentHealth -= damage;

        FindObjectOfType<AudioManager>().Play("Player Hurt");

        StartCoroutine(DamageAnimation());
        anim.SetTrigger("Hurt");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.transform.position.x > transform.position.x)
            {
                rigidb2D.velocity = new Vector2(-hurtMove, hurtMoveUp);
            }
            else
            {
                rigidb2D.velocity = new Vector2(hurtMove, hurtMoveUp);
            }
        }

        if (CurrentHealth <= 0)
        {
            Die();
            StartCoroutine(GameOverScene());
            FindObjectOfType<GameScenes>().gameHasOver = true;

        }
    }
    public class HealthChangedEventArgs : EventArgs
    {
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }
    }
    public void Die()
    {
        GetComponent<Vanish>().isVanishing = true;
        GetComponent<GhostTrail>().enabled = false;
    }
    IEnumerator GameOverScene()
    {
        Time.timeScale = 0.3f;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;
        FindObjectOfType<GameScenes>().FailLevel();

        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("Player Dead");
    }
    IEnumerator DamageAnimation()
    {
        srs.color = Color.red;
        movement.enabled = false;
        yield return new WaitForSeconds(.15f);

        srs.color = Color.black;
        movement.enabled = false;
        yield return new WaitForSeconds(.15f);

        srs.color = Color.red;
        movement.enabled = false;
        yield return new WaitForSeconds(.15f);

        srs.color = colorDefault;
        movement.enabled = true;
        yield return new WaitForSeconds(.15f);
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        CurrentHealth = data.hp;
    }
}