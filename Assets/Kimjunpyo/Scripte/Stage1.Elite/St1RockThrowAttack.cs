using UnityEngine;
using System.Collections;

public class St1RockThrowAttack : St1EliteState
{
    private St1EliteController elite;
    private Vector3 originalPosition; // 원래 위치 저장
    private Animator animator; // 애니메이터 참조

    public void OnEnter(St1EliteController elite)
    {
        this.elite = elite;
        this.animator = elite.animator; // 애니메이터 초기화
        originalPosition = elite.transform.position;

        if (animator != null)
        {
            animator.SetTrigger("Attack"); // 애니메이션 트리거 실행
        }

        elite.StartCoroutine(PerformRockThrow());
    }

    IEnumerator PerformRockThrow()
    {
        float followDuration = 2.517f;
        float elapsedTime = 0f;

        while (elapsedTime < followDuration)
        {
            Vector3 targetPos = elite.player.position + new Vector3(0, 7f, 0);
            elite.transform.position = Vector3.MoveTowards(
                elite.transform.position,
                targetPos,
                elite.followSpeed * Time.deltaTime
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 경고 이펙트 생성 (플레이어 중심 아래)
        GameObject warningEffect = GameObject.Instantiate(
            elite.warningEffectPrefab,
            elite.player.position,
            Quaternion.identity
        );
        warningEffect.transform.localScale = new Vector3(4f, 4f, 1f);
        GameObject.Destroy(warningEffect, 1.5f);

        yield return new WaitForSeconds(1f); // 시전 대기 시간

        // 락이다 생성 (플레이어 머리 위에서 떨어짐)
        Vector3 throwPosition = elite.player.position + new Vector3(0, 5f, 0);
        GameObject rock = GameObject.Instantiate(elite.rockPrefab, throwPosition, Quaternion.identity);
        rock.GetComponent<Rock>().Initialize(elite.impactEffectPrefab); // 타겟 위치 없이 이펙트만 전달

        yield return new WaitForSeconds(1.5f); // 낙하 시간

        // 원래 위치로 부드럽게 복귀
        elapsedTime = 0f;
        Vector3 startPos = elite.transform.position;
        while (elapsedTime < 1f)
        {
            elite.transform.position = Vector3.Lerp(startPos, originalPosition, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elite.transform.position = originalPosition;

        // 상태 전환
        elite.ChangeState(new St1IdleState());

        // 이동 애니메이션 실행 (선택)
        if (animator != null)
        {
            animator.SetTrigger("Move");
        }
    }

    public void OnUpdate() { }

    public void OnExit()
    {
        Debug.Log("Stage 1 Elite가 '락이다' 공격 상태를 종료합니다.");
    }
}
