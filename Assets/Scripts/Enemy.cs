using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Current Value")]
    [SerializeField] protected int hp;

    [Header("Init Value")]
    [SerializeField] private int hpMax = 1;

    [Header("Constant Value")]
    [SerializeField] private float speed = 1f;

    [Header("UI")]
    [SerializeField] GameObject canvasGO;
    [SerializeField] private Image guage;

    private Transform player;
    private EnemyContainer container;
    private Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }

    public virtual void Spawn(Vector3 position, Quaternion rotation, EnemyContainer container)
    {
        hp = hpMax;
        guage.fillAmount = 1f;
        canvasGO.SetActive(false);

        transform.position = position;
        transform.rotation = rotation;

        player = Player.Instance.transform;
        this.container = container;
    }
    public virtual void Die()
    {
        gameObject.SetActive(false);
        container.Charge(this);

        player = null;
        container = null;
    }
    protected virtual void Move()
    {
        if (player == null)
        {
            return;
        }

        if (rigidbody.linearVelocity != Vector2.zero)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }
}
