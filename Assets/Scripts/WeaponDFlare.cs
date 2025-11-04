using System.Collections;
using UnityEngine;

public class WeaponDFlare : Weapon
{
    [SerializeField] private int knockbackPower = 1;

    private ParticleSystem particle;
    private BoxCollider2D collider;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        collider = GetComponent<BoxCollider2D>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(power + Player.Instance.Strength);
            enemy.Knockback((enemy.transform.position - transform.position).normalized * knockbackPower);
        }
    }
    public override void Strengthen()
    {
        base.Strengthen();

        knockbackPower++;
    }
    public void Explode()
    {
        StartCoroutine(Cor());

        IEnumerator Cor()
        {
            particle.Play();
            collider.enabled = true;
            yield return new WaitForSeconds(0.15f);
            collider.enabled = false;
        }
    }
    public void Init(int knockbackPower)
    {
        this.knockbackPower = knockbackPower;
    }
}
