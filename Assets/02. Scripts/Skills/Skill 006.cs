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

        // 데미지, 사거리, 궁극기 여부 설정
        Missile missileScript = missile.GetComponent<Missile>();
        missileScript.damage = totalAtk;
        missileScript.range = totalRange;
        missileScript.ultmit = ultmit;
        missileScript.target = target; // 필요시 이 줄도 유지

        // 방향 벡터 계산
        Vector3 direction = (target.transform.position - missile.transform.position).normalized;

        // ✅ 회전도 여기서 적용
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 속도 적용
        Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * totalAtkSpeed;
        }
    }
}
