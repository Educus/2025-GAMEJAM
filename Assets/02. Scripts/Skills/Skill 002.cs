using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill002 : Skill
{
    [SerializeField] private GameObject miniCatPrefab;
    private float attackInterval = 15f;

    private bool isRunning = false;

    private void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(AttackLoop());
        }
    }
    protected override void Update()
    {
        return;
    }

    public override void Attack()
    {
        StartCoroutine(SpawnMiniCats());
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            Attack();
            Debug.Log("tod");
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private IEnumerator SpawnMiniCats()
    {
        int spawnCount = 2;
        float spawnRadius = 5f;
        float duration = 5f;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPos = (Vector2)transform.position + offset;

            GameObject miniCat = Instantiate(miniCatPrefab, spawnPos, Quaternion.identity);
            Destroy(miniCat, duration);
        }

        yield return null;
    }
}
