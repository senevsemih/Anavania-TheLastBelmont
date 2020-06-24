using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Transform attackCheck;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    [Range(0, 1)] [SerializeField] private float attackRadious;
    [Range(5, 30)] [SerializeField] public int attackDamage;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K) && !movement.crouch && !movement.jump)
            {
                Attack();
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K) && !movement.crouch && movement.jump)
            {
                Attack();
                animator.SetTrigger("JumpAttack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K) && movement.crouch && !movement.jump)
            {
                Attack();
                animator.SetTrigger("CrouchAttack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }
    public void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRadious, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Health>().TakeDamage(attackDamage);

            FindObjectOfType<AudioManager>().Play("Player Attack");
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackCheck == null)
            return;

        Gizmos.DrawWireSphere(attackCheck.position, attackRadious);
    }
}
