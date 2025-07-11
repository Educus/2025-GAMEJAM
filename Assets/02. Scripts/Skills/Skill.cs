using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public SoSkill soSkill;

    private string enemyTag = "Enemy";
    protected float cooltime = 0;


    protected virtual void Update()
    {
        cooltime += Time.deltaTime;

        if (cooltime >= soSkill.mCooltime)
        {
            cooltime = 0;
            Attack();
        }
    }
    abstract public void Attack();

    protected GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    protected GameObject FindClosestEnemys()
    {
        float range = soSkill.mProjectileDistance;  // 탐지 반경
        float clusterRadius = 3f;                   // 군집 판정 거리

        // 2D 감지
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        List<Transform> enemies = new List<Transform>();

        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
                enemies.Add(col.transform);
        }

        if (enemies.Count == 0)
            return null;

        // 군집 분석
        int maxCount = 0;
        Vector2 bestClusterCenter = Vector2.zero;
        List<Transform> bestCluster = new List<Transform>();

        foreach (Transform centerCandidate in enemies)
        {
            List<Transform> currentCluster = new List<Transform>();
            int count = 0;
            Vector2 sum = Vector2.zero;

            foreach (Transform other in enemies)
            {
                if (Vector2.Distance(centerCandidate.position, other.position) <= clusterRadius)
                {
                    currentCluster.Add(other);
                    sum += (Vector2)other.position;
                    count++;
                }
            }

            if (count > maxCount)
            {
                maxCount = count;
                bestCluster = currentCluster;
                bestClusterCenter = sum / count;
            }
        }

        // 군집 중심에서 가장 가까운 적 찾기
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform enemy in bestCluster)
        {
            float dist = Vector2.Distance(bestClusterCenter, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy.gameObject;
            }
        }

        return closest;
    }
}
