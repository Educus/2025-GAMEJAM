using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;                     // 웨이브 리스트
    [SerializeField] private Transform[] spawnPoints;              // 적 스폰 위치
    [SerializeField] private float spawnInterval = 30f;            // 웨이브 간격 시간

    public static WaveManager Instance;

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
    private float timer;
    private bool isWaveSpawning = false;

    private void Update()
    {
        if (isWaveSpawning) return;

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
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
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject prefabToSpawn = enemyData.overridePrefab != null
                    ? enemyData.overridePrefab
                    : enemyData.enemySO.mEnemyPrefab;

                GameObject enemyObj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

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
    }
}
