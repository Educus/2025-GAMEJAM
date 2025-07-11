using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill001 : Skill
{
    [SerializeField] private Geteling geteling;

    protected override void Update()
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
        GameObject target = FindClosestEnemys();

        geteling.parent = gameObject;
        geteling.target = target;
    }
}
