using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject capturePoint;
    [SerializeField] private List<Wave> waves;                     // 웨이브 리스트
    [SerializeField] private Vector2[] spawnPoints;              // 적 스폰 위치
    [SerializeField] private float spawnInterval = 15f;            // 웨이브 간격 시간
    [SerializeField] private Collider2D col;                        // 맵

    public static WaveManager Instance;

    private bool isClearTriggered = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SpawnWaveExternal()
    {
        StartCoroutine(SpawnWave());
    }


    private int currentWaveIndex = 0;
    private float timer = 10;
    private bool isWaveSpawning = false;

    private void Start()
    {
        SpawnPoints();
        SpawnCapturePoint();
    }

    private void SpawnPoints()
    {
        spawnPoints = new Vector2[5];

        Bounds bounds = col.bounds;

        int count = 5;
        int foundCount = 0;
        int tries = 0;
        int maxTries = count * 10;  // 무한루프 방지

        while (foundCount < count && tries < maxTries)
        {
            tries++;

            float x = Random.Range(bounds.min.x / 2, bounds.max.x / 2);
            float y = Random.Range(bounds.min.y / 2, bounds.max.y / 2);
            Vector2 candidate = new Vector2(x, y);

            if (col.OverlapPoint(candidate))
            {
                spawnPoints[foundCount] = candidate;
                foundCount++;
            }
        }

        if (foundCount < count)
        {
            Debug.LogWarning($"요청한 {count}개 위치 중 {foundCount}개만 찾았습니다.");
        }
    }

    private void SpawnCapturePoint()
    {
        Instantiate(capturePoint, spawnPoints[0], Quaternion.identity);
        Instantiate(capturePoint, spawnPoints[1], Quaternion.identity);
        Instantiate(capturePoint, spawnPoints[2], Quaternion.identity);
    }


    private void Update()
    {
        if (isWaveSpawning) return;

        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        bool isLastWave = currentWaveIndex >= waves.Count - 1;

        if (isLastWave && enemyCount == 0 && !isClearTriggered)
        {
            isClearTriggered = true;
            Clear();
            return;
        }

        if (enemyCount == 0 && currentWaveIndex < waves.Count)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0;
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            timer = 0;
        }
    }

    public IEnumerator SpawnWave()
    {
        isWaveSpawning = true;

        if (waves.Count == 0) yield break;

        Wave wave = waves[Mathf.Min(currentWaveIndex, waves.Count - 1)];

        foreach (var enemyData in wave.enemies)
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject prefabToSpawn = enemyData.overridePrefab != null
                    ? enemyData.overridePrefab
                    : enemyData.enemySO.mEnemyPrefab;

                GameObject enemyObj = Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);

                // overridePrefab이 null일 때만 SO 방식으로 세팅
                if (enemyData.overridePrefab == null)
                {
                    Enemy enemy = enemyObj.GetComponent<Enemy>();
                    enemy.SetData(enemyData.enemySO, wave.hpMultiplier, wave.atkMultiplier);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        if (currentWaveIndex < waves.Count - 1)
            currentWaveIndex++;

        isWaveSpawning = false;
        SpawnPoints();
    }

    private void Clear()
    {
        GameManager.Instance.StageClear();
    }

}
