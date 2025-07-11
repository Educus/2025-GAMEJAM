using UnityEngine;

public class HwaIdleState : IHwaBossState
{
    private HwaBossController boss;

    public void OnEnter(HwaBossController boss)
    {
        this.boss = boss;
        Debug.Log("보스가 대기 상태에 진입했습니다.");
    }

    public void OnUpdate()
    {
        // 플레이어와의 거리 확인
        if (boss.DistanceToPlayer() > boss.attackRange)
        {
            // 플레이어를 향해 이동
            boss.MoveTowardsPlayer();
        }
    }

    public void OnExit()
    {
        Debug.Log("보스가 대기 상태를 종료합니다.");
    }
}