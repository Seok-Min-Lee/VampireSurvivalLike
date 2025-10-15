using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField] private int speed;

    private Rigidbody2D rigidbody;
    private WeaponContainerB container;
    private float timer = 0f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (timer > 1f)
        {
            OnReload();

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    public void Init(WeaponContainerB container)
    {
        this.container = container;
    }
    
    public void Shot(Vector3 position, Quaternion rotation, Vector3 direction)
    {
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }
    public void OnReload()
    {
        rigidbody.angularDamping = 0f;
        rigidbody.angularVelocity = 0f;

        gameObject.SetActive(false);
        container.Reload(this);
    }
}
