using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill000 : Skill
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    public string enemyTag = "Enemy";

    private float cooltime = 0;
    void Update()
    {
        cooltime += Time.deltaTime;

        if (cooltime >= soSkill.mCooltime)
        {
            cooltime = 0;
            Attack();
        }
    }

    public override void Attack()
    {
        GameObject target = FindClosestEnemy();

        if (target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Vector3 direction = (target.transform.position - transform.position).normalized;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.velocity = direction * bulletSpeed;
            }
            else
            {
                bullet.transform.forward = direction;
            }
        }
    }

    private GameObject FindClosestEnemy()
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



}
