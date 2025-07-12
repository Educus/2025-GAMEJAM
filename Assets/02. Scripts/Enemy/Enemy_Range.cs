using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range : Enemy
{
    [SerializeField] private GameObject projectilePrefab;

    private float time = 0;

    protected override void Update()
    {
        base.Update();

        time -= Time.deltaTime;

        if (time <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Distance() > enemyData.mAtkRange) return;

        if (target == null) return;

        Vector3 shootPos = transform.position; // �ڽ� ��ġ���� �߻�

        // ���� ��� (�ڽ� �� Ÿ��)
        Vector2 direction = (target.transform.position - shootPos).normalized;

        // ����ü ����
        GameObject projectile = Instantiate(projectilePrefab, shootPos, Quaternion.identity);
        projectile.GetComponent<Enemy_projectile>().atk = enemyData.mAtk;

        // ����ü ȸ�� ���� (Z�� ȸ��)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Rigidbody2D�� ������ �ӵ� ����
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 5f;
        }

        time = enemyData.mAtkSpeed;
    }

    protected override void MoveToTarget()
    {
        if (Distance() < (enemyData.mAtkRange / 2))
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        base.MoveToTarget();
    }

    private float Distance()
    {
        return Vector2.Distance(target.transform.position, transform.position);
    }

}
