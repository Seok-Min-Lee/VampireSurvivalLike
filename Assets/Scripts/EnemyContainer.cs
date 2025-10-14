using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private int spawnInterval;
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float maxSpawnDistance = 20f;

    [SerializeField] private int poolSizeMin = 0;
    [SerializeField] private int poolSizeMax = 30;

    private int count;

    public Queue<Enemy> pool { get; private set; } = new Queue<Enemy>();
    private void Start()
    {
        Init();
    }

    private float timer = 0f;
    private void Update()
    {
        if (timer > spawnInterval)
        {
            Spawn();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    public void Charge(Enemy enemy)
    {
        pool.Enqueue(enemy);
        count--;
    }

    private void Spawn()
    {
        if (count < 30)
        {
            // 
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

            Vector2 position = (Vector2)Player.Instance.transform.position + direction * distance;

            //
            Enemy enemy;
            if (pool.Count > 0)
            {
                enemy = pool.Dequeue();
                enemy.gameObject.SetActive(true);
            }
            else
            {
                enemy = Instantiate<Enemy>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
            }

            enemy.Spawn(position: position, rotation: Quaternion.identity, container: this);

            count++;
        }
    }
    private void Init()
    {
        for (int i = 0; i < poolSizeMin; i++)
        {
            Enemy enemy = Instantiate(enemyPrefabs[0], transform).GetComponent<Enemy>();
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Player.Instance == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Player.Instance.transform.position, maxSpawnDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.Instance.transform.position, minSpawnDistance);
    }
}
