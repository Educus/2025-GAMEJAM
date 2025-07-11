using UnityEngine;
using System.Collections;

public class St2HitAttack : St2EliteState
{
    private St2EliteController elite;

    public void OnEnter(St2EliteController elite)
    {
        this.elite = elite;
        elite.StartCoroutine(PerformAttack());
    }

    IEnumerator PerformAttack()
    {
        for (int i = 0; i < 3; i++) // 3번 공격
        {
            // 플레이어의 현재 위치를 실시간으로 가져옴
            Vector3 attackPosition = elite.player.position;

            // 경고 이펙트 생성
            GameObject warningEffect = GameObject.Instantiate(elite.warningEffectPrefab, attackPosition, Quaternion.identity);
            warningEffect.transform.localScale = new Vector3(elite.attackRange, elite.attackRange, 1f);
            GameObject.Destroy(warningEffect, 0.5f); // 경고 이펙트 제거

            yield return new WaitForSeconds(0.5f); // 시전 대기 시간

            // 공격 판정
            Collider2D[] targets = Physics2D.OverlapCircleAll(attackPosition, elite.attackRange);
            foreach (var col in targets)
            {
                if (col.CompareTag("Player"))
                {
                    col.GetComponent<Player>()?.IHit(15); // 플레이어에게 데미지
                }
            }

            yield return new WaitForSeconds(0.5f); // 공격 간격
        }

        elite.ChangeState(new St2IdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}

    public void OnExit()
    {
        Debug.Log("엘리트가 공격 상태를 종료합니다.");
    }
}