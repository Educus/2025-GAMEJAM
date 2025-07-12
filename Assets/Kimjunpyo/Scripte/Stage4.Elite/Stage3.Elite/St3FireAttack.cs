using UnityEngine;
using System.Collections;

public class St3FireAttack : St3EliteState
{
    private St3EliteController elite3;

    public void OnEnter(St3EliteController elite)
    {
        this.elite3 = elite;
        elite3.StartCoroutine(PerformFireAttack());
    }

    IEnumerator PerformFireAttack()
    {
        // 경고 선 생성
        Vector3 direction = (elite3.player.position - elite3.transform.position).normalized;
        for (int i = -1; i <= 1; i++) // 3갈래 화염
        {
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, i * 30) * direction;
            GameObject warningLine = GameObject.Instantiate(elite3.warningLinePrefab, elite3.transform.position, Quaternion.identity);
            warningLine.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedDirection);
            GameObject.Destroy(warningLine, 0.5f); // 경고 선 제거
        }

        yield return new WaitForSeconds(0.5f); // 시전 대기 시간

        // 화염 발사
        for (int i = -1; i <= 1; i++) // 3갈래 화염
        {
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, i * 30) * direction;
            GameObject fire = GameObject.Instantiate(elite3.firePrefab, elite3.transform.position, Quaternion.identity);
            fire.GetComponent<Rigidbody2D>().velocity = rotatedDirection * elite3.fireSpeed;
        }

        elite3.ChangeState(new St3IdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}

    public void OnExit()
    {
        Debug.Log("Stage 3 Elite가 화염 공격 상태를 종료합니다.");
    }
}