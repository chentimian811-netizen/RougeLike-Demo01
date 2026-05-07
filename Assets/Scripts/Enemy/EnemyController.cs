using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxhealth = 20;
    public int damage;
    public int health;

    private EnemyHealthBar healthBar;


    [Header("属性")]
    [SerializeField] private float currentSpeed = 0f;
    private Vector2 moveInput;
    private PlayerHealth playerHealth;
    [Header("攻击")]
    [SerializeField] private bool isAttact = true;
    [SerializeField] private float AttackCoolDuration = 1f;

    public Vector2 MovementInput { get; set; }

    private bool dead;
    private Rigidbody2D rb;
    private SpriteRenderer rs;
    private Animator anim;


    private PickSpawner pickSpawner;//掉落物品脚本

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        health = maxhealth;

        InitializeHealthBar();


    }
    private void InitializeHealthBar()
    {
        if (healthBar != null)
        {
            // 确保血条激活
            healthBar.gameObject.SetActive(true);
            healthBar.UpdateHealthBar();
        }
    }

    private void Awake()
    {
        pickSpawner = GetComponent<PickSpawner>();//获取掉落物品脚本组件

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rs = GetComponent<SpriteRenderer>();

        //healthBar = GetComponentInChildren<EnemyHealthBar>();
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<EnemyHealthBar>(true);
        }
    }


    private void Update()
    {
        if (healthBar != null && !dead)
        {
            healthBar.UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {


        if (dead) return;

        // 减少血量
        health -= damage;

        // 确保血量不低于0
        health = Mathf.Max(health, 0);

        // 更新血条
        UpdateHealthBar();

        //// 受伤效果（可以添加闪烁效果）
        //StartCoroutine(DamageEffect());

        // 死亡检查
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }
    }


    private void Die()
    {
        dead = true;

        //// 播放死亡动画
        //if (anim != null)
        //{
        //    anim.SetTrigger("Die");
        //}

        // 禁用碰撞和移动
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 隐藏血条
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }


        pickSpawner.DropItems();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!dead)
            Move();
        SetAnimation();
    }
    void Move()
    {
        if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
        {
            rb.velocity = MovementInput * currentSpeed;

            if (MovementInput.x < 0)
            {
                rs.flipX = false;
            }
            else if (MovementInput.x > 0)
            {
                rs.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        if (isAttact)
        {
            isAttact = false;
            StartCoroutine(nameof(AttackCoroutine));
        }
    }
    IEnumerator AttackCoroutine()
    {
        anim.SetTrigger("isAttack");

        yield return new WaitForSeconds(AttackCoolDuration);
        isAttact = true;
    }

    void SetAnimation()
    {
        anim.SetBool("isRun", MovementInput.magnitude > 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }

    //public void EnemyDead()
    //{
    //    dead = true;
    //}

    //public void EnemyHurt() 
    //{
    //    anim.SetTrigger("isHurt");
    //}

    //public void DestroyEnemy()
    //{
    //    Destroy(this.gameObject); 
    //} 
}

