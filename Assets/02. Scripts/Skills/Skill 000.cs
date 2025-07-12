using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill000 : Skill
{
    [SerializeField] private GameObject bulletPrefab;

    public override void Attack()
    {
        GameObject target = FindClosestEnemy();

        if (target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,1.6f), Quaternion.identity);
            bullet.GetComponent<Bullet>().start = gameObject;
            bullet.GetComponent<Bullet>().damage = totalAtk;
            bullet.GetComponent<Bullet>().range = totalRange;

            Vector3 direction = (target.transform.position - bullet.transform.position).normalized;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.velocity = direction * totalAtkSpeed;
            }
            else
            {
                bullet.transform.forward = direction;
            }
        }
    }

    
}
