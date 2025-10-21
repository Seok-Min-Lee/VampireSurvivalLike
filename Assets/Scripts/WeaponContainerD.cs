using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponContainerD : WeaponContainer<WeaponD>
{
    private Queue<WeaponD> bulletPool = new Queue<WeaponD>();
    private float timer = 0f;
    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Add();
        }

        if (timer > 1f)
        {
            Launch();

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    public override void Add()
    {
        if (activeCount >= WEAPON_COUNT_MAX)
        {
            return;
        }
        activeCount++;
    }

    public void Launch()
    {
        for (int i = 0; i < activeCount; i++)
        {
            WeaponD bullet;
            if (bulletPool.Count > 0)
            {
                bullet = bulletPool.Dequeue();
            }
            else
            {
                bullet = GameObject.Instantiate<WeaponD>(prefab, transform);
                bullet.Init(this);
            }

            float radian = Random.Range(60, 120) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            bullet.Shot(
                position: transform.position,
                rotation: transform.rotation,
                direction: direction
            );
        }
    }
    public void Reload(WeaponD bullet)
    {
        bulletPool.Enqueue(bullet);
    }
}
