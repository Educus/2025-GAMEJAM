using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill006 : Skill
{
    [SerializeField] private GameObject missilePrefab;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (soSkill.isSkill1) { bonusRange = 1; }
        if (soSkill.isSkill2) { bonusAtk = 5; }
        if (soSkill.isSkill3) { ultmit = true; }
    }
    public override void Attack()
    {
        GameObject target = FindClosestEnemys();

        if (target == null) target = FindClosestEnemy();
        if (target == null) return;

        GameObject missile = Instantiate(missilePrefab, transform.position + new Vector3(0, 1.6f), Quaternion.identity);
        missile.GetComponent<Missile>().damage = totalAtk;
        missile.GetComponent<Missile>().range = totalRange;
        missile.GetComponent<Missile>().ultmit = ultmit;

        Vector3 direction = (target.transform.position - missile.transform.position).normalized;

        Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * totalAtkSpeed;
        }
        else
        {
            missile.transform.forward = direction;
        }
    }
}
