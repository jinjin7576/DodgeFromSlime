using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer;
    public float enemySpawnTime;
    public float maxLimit;
    public float minLimit;

    public GameObject enemyPrefab;
    public List<GameObject> enemys;
    void Start()
    {
        enemys = new List<GameObject>();
        enemySpawnTime = 0;
    }


    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            timer += Time.deltaTime;
            GameObject enemy = SpawnEnemy();
            if (enemy != null)
            {
                TransformEnemy(enemy);
            }
        }
    }
    GameObject SpawnEnemy()
    {
        if (timer >= enemySpawnTime)
        {
            timer = 0;
            enemySpawnTime = Random.Range(minLimit, maxLimit);
            foreach (GameObject enemy in enemys)
            {
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(true);
                    return enemy;
                }
            }
            GameObject newEnemy = Instantiate(enemyPrefab);
            enemys.Add(newEnemy);
            return newEnemy;
        }
        return null;
    }
    void TransformEnemy(GameObject enemy)
    {
        float limtMinY = -4f;
        float limtMaxY = 3f;
        Vector2 spawnPos = new Vector2(transform.position.x, Random.Range(limtMinY, limtMaxY));

        enemy.transform.position = spawnPos;
    }
}
