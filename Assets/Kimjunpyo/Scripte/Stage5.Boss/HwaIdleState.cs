using UnityEngine;

public class HwaIdleState : IHwaBossState
{
    private HwaBossController boss;
    private float idleTimer = 0f;
    private float idleDuration = 3f;

    public void OnEnter(HwaBossController boss)
    {
        this.boss = boss;
        idleTimer = 0f;
        Debug.Log("보스가 대기 상태에 진입했습니다.");
    }

    public void OnUpdate()
    {
        idleTimer += Time.deltaTime;

        if (boss.DistanceToPlayer() > boss.attackRange)
        {
            boss.MoveTowardsPlayer();
        }
        else
        {
            if (idleTimer >= idleDuration)
            {
                boss.ChangeState(new HwaEarthquakeAttack()); // 혹은 랜덤 스킬 호출하는 함수
            }
        }
    }

    public void OnExit()
    {
        Debug.Log("보스가 대기 상태를 종료합니다.");
    }
}
