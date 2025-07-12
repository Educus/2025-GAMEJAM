using UnityEngine;
using System.Collections;

public class St1RockThrowAttack : St1EliteState
{
    private St1EliteController elite;
    private Vector3 originalPosition; // 원래 위치 저장
    

    public void OnEnter(St1EliteController elite)
    {
        this.elite = elite;
        originalPosition = elite.transform.position; // 원래 위치 저장
        elite.StartCoroutine(PerformRockThrow());
    }

    IEnumerator PerformRockThrow()
{
    float followDuration = 2f;
    float elapsedTime = 0f;

    while (elapsedTime < followDuration)
    {
        Vector3 targetPos = elite.player.position + new Vector3(0, 3f, 0);
        elite.transform.position = Vector3.MoveTowards(
            elite.transform.position,
            targetPos,
            elite.followSpeed * Time.deltaTime // 따라다니는 속도 (속도 조정 가능)
        );

        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // 경고 이펙트 생성 (플레이어 정중앙 아래)
    GameObject warningEffect = GameObject.Instantiate(elite.warningEffectPrefab, elite.player.position, Quaternion.identity);
    warningEffect.transform.localScale = new Vector3(4f, 4f, 1f);
    GameObject.Destroy(warningEffect, 1.5f);

    yield return new WaitForSeconds(1f); // 시전 대기 시간

    // 락이다 생성 (플레이어 바로 위에 낙하)
    Vector3 throwPosition = elite.player.position + new Vector3(0, 3f, 0);
    GameObject rock = GameObject.Instantiate(elite.rockPrefab, throwPosition, Quaternion.identity);
    rock.GetComponent<Rock>().Initialize(elite.player.position, elite.impactEffectPrefab);

    yield return new WaitForSeconds(1.5f); // 낙하 시간

    // 원래 위치로 복귀 (1초 동안 부드럽게)
    elapsedTime = 0f;
    Vector3 startPos = elite.transform.position;
    while (elapsedTime < 1f)
    {
        elite.transform.position = Vector3.Lerp(startPos, originalPosition, elapsedTime / 1f);
        elapsedTime += Time.deltaTime;
        yield return null;
    }
    elite.transform.position = originalPosition;

    elite.ChangeState(new St1IdleState());
}

    public void OnUpdate() {}

    public void OnExit()
    {
        Debug.Log("Stage 1 Elite가 '락이다' 공격 상태를 종료합니다.");
    }
}
