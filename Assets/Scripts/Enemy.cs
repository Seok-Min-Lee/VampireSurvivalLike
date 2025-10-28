using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject character; 

    [SerializeField] [Range(0, 7)] private int itemIndex;

    [Header("Current Value")]
    [SerializeField] protected int hp = 1;
    [SerializeField] private int hpMax = 1;
    [SerializeField] protected int power;
    [SerializeField] protected float speed;
    [SerializeField] protected int addictDamage = 0;
    [SerializeField] protected bool isAddict = false;

    [Header("Default Value")]
    [SerializeField] private int hpDefault = 1;
    [SerializeField] private int powerDefault = 1;
    [SerializeField] private float speedDefault = 1f;

    [Header("level Value")]
    [SerializeField] private int hpLevel = 0;
    [SerializeField] private int powerLevel = 0;
    [SerializeField] private int speedLevel = 0;

    [Header("UI")]
    [SerializeField] GameObject canvasGO;
    [SerializeField] private Image guage;

    private Transform target;
    private EnemyPool pool;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float addictTimer = 0f;
    private float knockbackTimer = 0f;

    private bool isDead = false;
    private bool isKnockback = false;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        spriteRenderer = character.GetComponent<SpriteRenderer>();
        animator = character.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isAddict)
        {
            if (addictTimer > 1f)
            {
                OnDamage(addictDamage);
                addictTimer = 0f;
            }

            addictTimer += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(SoundKey.PlayerHit);
            Player.Instance.OnDamage(power);
            Die();
        }
    }

    public virtual void Spawn(EnemyPool pool, int hpLevel, int powerLevel, int speedLevel, Vector3 position, Quaternion rotation)
    {
        isDead = false;

        this.pool = pool;
        this.hpLevel = hpLevel;
        this.powerLevel = powerLevel;
        this.speedLevel = speedLevel;

        hpMax = (int)(hpDefault * (1 + 0.1f * hpLevel));
        power = (int)(powerDefault * (1 + 0.1f * powerLevel));
        speed = speedDefault * (1 + 0.1f * speedLevel);

        hp = hpMax;
        guage.fillAmount = 1f;
        canvasGO.SetActive(false);

        transform.position = position;
        transform.rotation = rotation;

        target = Player.Instance.transform;
    }
    public virtual void Die()
    {
        isDead = true;

        OffAddict();

        gameObject.SetActive(false);
        pool.Charge(this);

        target = null;
        pool = null;
    }
    public virtual void OnDamage(int damage)
    {
        if (isDead) 
        {
            return;
        }

        AudioManager.Instance.PlaySFX(SoundKey.EnemyHit);
        hp -= damage;
        Knockback(Vector3.zero);

        if (hp > 0)
        {
            animator.SetTrigger("doHit");

            canvasGO.SetActive(true);
            guage.fillAmount = (float)hp / (float)hpMax;
        }
        else
        {
            Die();

            if (ItemContainer.Instance != null)
            {
                ItemContainer.Instance.Batch(itemIndex, transform.position);
            }

            if (Player.Instance != null)
            {
                Player.Instance.KillEnemy();
            }
        }
    }
    public virtual void OnAddict(int value)
    {
        isAddict = true;
        addictDamage = value;
        addictTimer = 0f;
    }
    public virtual void OffAddict()
    {
        addictTimer = 0f;
        addictDamage = 0;
        isAddict = false;
    }
    protected virtual void Move()
    {
        if (target == null)
        {
            return;
        }

        if (isKnockback)
        {
            knockbackTimer += Time.deltaTime;

            if (knockbackTimer > .2f)
            {
                knockbackTimer = 0f;
                isKnockback = false;
                rigidbody.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            if (rigidbody.linearVelocity != Vector2.zero)
            {
                rigidbody.linearVelocity = Vector2.zero;
            }

            Vector2 dir = (target.position - transform.position).normalized;
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
            spriteRenderer.flipX = dir.x > 0;
        }
    }
    protected virtual void Knockback(Vector3 direction)
    {
        if (isKnockback)
        {
            return;
        }

        rigidbody.AddForce(direction, ForceMode2D.Impulse);
        isKnockback = true;
    }
}
