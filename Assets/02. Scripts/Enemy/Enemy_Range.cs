using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range : Enemy
{
    protected override void Update()
    {
        base.Update();

        Attack();
    }

    private void Attack()
    {
        if (Distance() > enemyData.mAtkRange) return;

    }

    protected override void MoveToTarget()
    {
        if (Distance() < (enemyData.mAtkRange / 2)) return;

        base.MoveToTarget();
    }

    private float Distance()
    {
        return Vector2.Distance(target.transform.position, transform.position);
    }

}
