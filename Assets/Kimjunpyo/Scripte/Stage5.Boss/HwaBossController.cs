using UnityEngine;

/// <summary>
/// 보스의 상태를 관리하는 컨트롤러.
/// </summary>
public class HwaBossController : MonoBehaviour
{
    public Transform player;               // 플레이어 위치
    public GameObject fireStonePrefab;     // 화염 돌 프리팹
    public GameObject warningFanFX;        // 경고 이펙트 프리팹

    private IHwaBossState currentState;    // 현재 상태

    public float attackRange = 10f;        // 공격 범위
    private float attackCooldown = 5f;     // 공격 쿨타임
    private float timer = 0f;              // 쿨타임 타이머

    private void Start()
    {
        ChangeState(new HwaIdleState()); // 초기 상태: 대기
    }

    private void Update()
    {
        timer += Time.deltaTime;
        currentState?.OnUpdate();

        // 쿨타임이 끝나면 랜덤 공격 상태로 전환
        if (timer >= attackCooldown)
        {
            timer = 0f;
            float rand = Random.value;
            if (rand < 0.5f)
                ChangeState(new HwaFireStoneAttack());
            else
                ChangeState(new HwaEarthquakeAttack());
        }
    }

    /// <summary>
    /// 상태를 변경합니다.
    /// </summary>
    public void ChangeState(IHwaBossState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    /// <summary>
    /// 플레이어와의 거리를 반환합니다.
    /// </summary>
    public float DistanceToPlayer()
    {
        return Vector2.Distance(transform.localPosition, player.localPosition);
    }
}
