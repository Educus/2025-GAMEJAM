using UnityEngine;
using System.Collections;

public class St4SlashAttack : St4EliteState
{
    private St4EliteController elite;

    public void OnEnter(St4EliteController elite)
    {
        this.elite = elite;
        elite.StartCoroutine(PerformSlashAttack());
    }

    IEnumerator PerformSlashAttack()
    {
        // 참격 생성 위치와 방향 계산
        Vector3 directionToPlayer = (elite.player.position - elite.transform.position).normalized;
        Vector3 slashPosition = elite.transform.position + directionToPlayer * 2f;

        // 참격의 회전 설정 (플레이어 방향으로 가로 직사각형)
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // 경고 이펙트 생성
        GameObject warningEffect = GameObject.Instantiate(elite.warningEffectPrefab, slashPosition, Quaternion.Euler(0, 0, angle));
        warningEffect.transform.localScale = new Vector3(5f, 1f, 1f); // 가로 5 유닛, 세로 1 유닛
        GameObject.Destroy(warningEffect, 1.5f); // 경고 이펙트 제거

        yield return new WaitForSeconds(1.5f); // 시전 대기 시간

        // 참격 생성
        GameObject slash = GameObject.Instantiate(elite.slashPrefab, slashPosition, Quaternion.Euler(0, 0, angle));
        slash.GetComponent<Slash>().Initialize(elite.player.position);

        elite.ChangeState(new St4IdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}

    public void OnExit()
    {
        Debug.Log("Stage 4 Elite가 참격 공격 상태를 종료합니다.");
    }
}