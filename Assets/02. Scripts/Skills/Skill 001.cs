using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill001 : Skill
{
    [SerializeField] private Geteling geteling;

    private bool isCooltime = false;

    protected override void Update()
    {
        if (isCooltime) return;

        cooltime += Time.deltaTime;

        if (cooltime >= totalCooltime)
        {
            isCooltime = true;
            Attack();
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (soSkill.isSkill1) { bonusAtk = 5; }
        if (soSkill.isSkill2) { bonusCooltime = 1; }
        if (soSkill.isSkill3) { ultmit = true; }
    }

    public override void Attack()
    {
        GameObject target = FindClosestEnemys();
        
        if (target == null) target = FindClosestEnemy();

        geteling.parent = gameObject;
        geteling.target = target;
        geteling.ultmit = ultmit;

        geteling.gameObject.SetActive(true);

        StartCoroutine(IETimer());
    }

    IEnumerator IETimer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("케틀링 비활성");
        Debug.Log(soSkill.mCooltime);
        Debug.Log(GameManager.Instance.player.playerStat.cooltime);
        Debug.Log(bonusCooltime);

        geteling.gameObject.SetActive(false);

        cooltime = 0;
        isCooltime = false;
    }

}
