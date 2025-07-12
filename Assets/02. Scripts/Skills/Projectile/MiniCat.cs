using System.Collections;
using UnityEngine;

public class MiniCat : MonoBehaviour
{
    [Header("����")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackInterval = 0.5f;
    [SerializeField] private float detectRange = 6f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float bulletRange = 6f;
    [SerializeField] private float bulletYOffset = 0.55f;

    private void Start()
    {
        StartCoroutine(AttackRoutine());
        Destroy(gameObject, 5f); // 5�� �� �ڵ� �ı�
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            GameObject target = FindNearestEnemy();

            if (target != null)
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;

                // �Ѿ� ���� ��ġ (y������ 0.55 ��)
                Vector3 spawnPosition = transform.position + new Vector3(0f, bulletYOffset, 0f);

                // �Ѿ� ���� �� ����
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.start = this.gameObject;
                    bulletScript.damage = damage;
                    bulletScript.range = bulletRange;
                }

                // �Ѿ˿� �ӵ� ����
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = direction * 8f;
            }

            yield return new WaitForSeconds(attackInterval);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject nearest = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < detectRange && dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }
}