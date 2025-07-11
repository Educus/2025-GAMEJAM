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
        // 2초에 걸쳐 Y축으로 3 유닛 상승
        Vector3 targetPosition = originalPosition + new Vector3(0, 3, 0);
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elite.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elite.transform.position = targetPosition; // 정확한 위치 설정

        // 경고 이펙트 생성
        GameObject warningEffect = GameObject.Instantiate(elite.warningEffectPrefab, elite.player.position, Quaternion.identity);
        warningEffect.transform.localScale = new Vector3(4f, 4f, 1f); // 지름 4 유닛
        GameObject.Destroy(warningEffect, 1.5f); // 경고 이펙트 제거

        yield return new WaitForSeconds(1f); // 시전 대기 시간

        // '락이다' 생성
        Vector3 throwPosition = elite.player.position + new Vector3(0, 3, 0);
        GameObject rock = GameObject.Instantiate(elite.rockPrefab, throwPosition, Quaternion.identity);
        rock.GetComponent<Rock>().Initialize(elite.player.position, elite.impactEffectPrefab); // 충돌 이펙트 전달

        yield return new WaitForSeconds(1.5f); // 락이다가 떨어지는 시간

        // 1초에 걸쳐 원래 위치로 복귀
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elite.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elite.transform.position = originalPosition; // 정확한 위치 설정

        elite.ChangeState(new St1IdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}

    public void OnExit()
    {
        Debug.Log("Stage 1 Elite가 '락이다' 공격 상태를 종료합니다.");
    }
}