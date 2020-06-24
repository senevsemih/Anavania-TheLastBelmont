using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [HideInInspector] public bool attackRange;
    public GameObject hitBox    ;
    public GameObject fireballProjectile;
    public Transform fireballPoint;
    [Space]
    [Header("Patrol Points")]
    public Transform leftPatrolLimit;
    public Transform rightPatrolLimit;
    [Space]
    [Header("Areas")]
    public GameObject lookArea;
    public GameObject triggerArea;
    [Space]
    [Header("Stats")]
    [Range(1, 50)] public float rangeAttackDistance;
    [Range(1, 50)] public float meleeAttackDistance;
    [Range(1, 50)] public float moveSpeed;
    [Range(1, 50)] public float cooldown;

    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool attackCooldown;
    private float timer;

    private void Awake()
    {
        SelectTarget();
        timer = cooldown;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !attackRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("melee_attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("range_attack"))
        {
            SelectTarget();
        }

        if (attackRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > rangeAttackDistance)
            StopAttack();
        else if (meleeAttackDistance >= distance && attackCooldown == false)
            MeleeAttack();
        else if (rangeAttackDistance >= distance && attackCooldown == false)
            RangeAttack();
        
        if (attackCooldown)
        {
            Cooldown();
            anim.SetBool("Range_Attack", false);
            anim.SetBool("Melee_Attack", false);

            hitBox.SetActive(false);
        }
    }
    void Move()
    {
        //walk anim true
        anim.SetBool("move", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee_attack") || !anim.GetCurrentAnimatorStateInfo(0).IsName("range_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    void MeleeAttack()
    {
        cooldown = timer;
        attackMode = true;

        //walk anim false
        anim.SetBool("move", false);


        anim.SetBool("Melee_Attack", true);
        anim.SetBool("Range_Attack", false);

        hitBox.SetActive(true);

        Debug.Log("Melee Attack True");
    }
    void RangeAttack()
    {
        cooldown = timer;
        attackMode = true;

        //walk anim false
        anim.SetBool("move", false);


        anim.SetBool("Range_Attack", true);
        anim.SetBool("Melee_Attack", false);

        hitBox.SetActive(true);

        Debug.Log("Range Attack True");
    }
    void StopAttack()
    {
        attackCooldown = false;
        attackMode = false;

        anim.SetBool("Range_Attack", false);
        anim.SetBool("Melee_Attack", false);

        hitBox.SetActive(false);

        Debug.Log("Attack False");
    }
    void Cooldown()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && attackCooldown && attackMode)
        {
            attackCooldown = false;
            cooldown = timer;
        }
    }
    public void TriggerCooldown()
    {
        attackCooldown = true;
    }
    public void TriggerFireball()
    {
        Instantiate(fireballProjectile, fireballPoint.position, Quaternion.identity);
    }
    bool InsideOfLimits()
    {
        return transform.position.x > leftPatrolLimit.position.x && transform.position.x < rightPatrolLimit.position.x;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftPatrolLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightPatrolLimit.position);

        if (distanceToLeft > distanceToRight)
            target = leftPatrolLimit;
        else
            target = rightPatrolLimit;

        Flip();
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
            rotation.y = 180f;
        else
            rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
}
