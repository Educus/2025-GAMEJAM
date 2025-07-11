using UnityEngine;

public class St3EliteController : MonoBehaviour
{
    public Transform player;               // 플레이어 위치
    public GameObject firePrefab;          // 화염 프리팹
    public GameObject warningLinePrefab;   // 경고 선 프리팹
    public float attackCooldown = 5f;      // 스킬 주기
    public float fireSpeed = 10f;          // 화염 속도
    public float attackRange = 8f;         // 공격 범위
    public float moveSpeed = 2f;           // 이동 속도
    private float timer = 0f;              // 쿨타임 타이머

    private St3EliteState currentState;    // 현재 상태

    private void Start()
    {
        ChangeState(new St3IdleState()); // 초기 상태: 대기
    }

    private void Update()
    {
        timer += Time.deltaTime;
        currentState?.OnUpdate();

        // 스킬 주기가 끝나면 공격 상태로 전환
        if (timer >= attackCooldown && currentState is St3IdleState)
        {
            timer = 0f;
            ChangeState(new St3FireAttack()); // 화염 공격 상태로 전환
        }
    }

    public void ChangeState(St3EliteState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public float DistanceToPlayer()
    {
        if (player == null) return float.MaxValue; // 플레이어가 없으면 최대 거리 반환
        return Vector2.Distance(transform.position, player.position);
    }

    public void MoveTowardsPlayer()
    {
        if (player == null) return; // 플레이어가 없으면 이동하지 않음

        Vector2 direction = (player.position - transform.position).normalized; // 방향 계산
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime); // 이동
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.localPosition, attackRange); // 공격 범위 시각화
    }
}