using UnityEngine;

public class St1IdleState : St1EliteState
{
    private St1EliteController elite;
    private float idleTimer = 0f; // 대기 타이머
    private float idleDuration = 3f; // 대기 시간 (3초)

    public void OnEnter(St1EliteController elite)
    {
        this.elite = elite;
        idleTimer = 0f; // 타이머 초기화
        Debug.Log("Stage 1 Elite가 대기 상태에 진입했습니다.");
    }

    public void OnUpdate()
    {
        idleTimer += Time.deltaTime;

        // 플레이어와의 거리 확인
        if (elite.DistanceToPlayer() > elite.attackRange)
        {
            elite.MoveTowardsPlayer(); // 플레이어를 향해 이동
        }
        else
        {
            // 대기 시간이 끝나면 공격 상태로 전환
            if (idleTimer >= idleDuration)
            {
                elite.ChangeState(new St1RockThrowAttack()); // '락이다' 공격 상태로 전환
            }
        }
    }

    public void OnExit()
    {
        Debug.Log("Stage 1 Elite가 대기 상태를 종료합니다.");
    }
}