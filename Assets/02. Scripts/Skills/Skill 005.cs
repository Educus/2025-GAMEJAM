using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill005 : Skill
{
    [SerializeField] private GameObject canBoomPrefab;

    private bool isSpawning = false;

    public override void Attack()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        float radius = 6f;
        float spawnDelay = 5f;

        // ���� ��ġ ���
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        Vector2 spawnPos = (Vector2)transform.position + randomOffset;

        // ������ ����
        if (canBoomPrefab != null)
        {
            Instantiate(canBoomPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Skill005: summonPrefab�� �������� �ʾҽ��ϴ�.");
        }

        // 5�� ���
        yield return new WaitForSeconds(spawnDelay);

        isSpawning = false;
    }
}
