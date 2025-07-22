using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public List<GameObject> platforms;
    float spawnTimer;
    float spawnTiming = 0;
    Vector2 lastSpawnPlatform;


    void Start()
    {
        platforms = new List<GameObject>();
    }


    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            GameObject acitvatePlatform = SpawnPlatform();
            if (acitvatePlatform != null)
            {

                TransformPlatform(acitvatePlatform);
            }
        }
    }

    GameObject SpawnPlatform()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTiming)
        {
            spawnTimer = 0f;
            spawnTiming = Random.Range(1f, 1.75f);

            // 1. ��Ȱ�� �÷����� �ִ��� Ž��
            foreach (GameObject platform in platforms)
            {
                if (!platform.activeInHierarchy)
                {
                    platform.SetActive(true);
                    return platform;
                }
            }

            // 2. ������ ���� ����
            GameObject newPlatform = Instantiate(platformPrefab);
            platforms.Add(newPlatform);
            return newPlatform;
        }

        return null; // ���� ���� �ð� �ƴ�
    }

    void TransformPlatform(GameObject platform)
    {
        float newY = Random.Range(-4f, 3f);

        // ���̰� �ʹ� ũ�� ������ ����
        float deltaY = Mathf.Abs(newY - lastSpawnPlatform.y);
        if (deltaY > 1.5f)
        {
            // lastSpawnPlatform.y �������� +/-1.5 ���� ������ clamp
            float minY = lastSpawnPlatform.y - 1.5f;
            float maxY = lastSpawnPlatform.y + 1.5f;

            // clamp ������ ��ü ���� �ȿ� ����
            minY = Mathf.Clamp(minY, -4f, 3f);
            maxY = Mathf.Clamp(maxY, -4f, 3f);

            newY = Random.Range(minY, maxY);
        }

        platform.transform.position = new Vector2(transform.position.x, newY);
        lastSpawnPlatform = platform.transform.position;
    }
}
