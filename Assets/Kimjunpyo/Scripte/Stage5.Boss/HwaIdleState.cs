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
        if (boss.DistanceToPlayer() <= boss.attackRange)
        {
            Debug.Log("플레이어가 공격 범위 내에 있습니다.");
            boss.ChangeState(new HwaFireStoneAttack()); // 기본 공격 상태로 전환
        }
    }

    public void OnExit()
    {
        Debug.Log("보스가 대기 상태를 종료합니다.");
    }
}