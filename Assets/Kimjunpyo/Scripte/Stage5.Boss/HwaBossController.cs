using UnityEngine;
using UnityEngine.Animations;

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
    public float moveSpeed = 3f;           // 이동 속도
    private float attackCooldown = 5f;     // 공격 대기 시간
    private float timer = 0f;              // 쿨타임 타이머
    public Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found!");
            }
        }
    }

    public void SelectNextSkill()
    {
        float random = Random.Range(0f, 1f);

        if (random < 0.5f)
            ChangeState(new HwaEarthquakeAttack());
        else
            ChangeState(new HwaFireStoneAttack());
    }

    private void Update()
{
    timer += Time.deltaTime;
    currentState?.OnUpdate();

    if (timer >= attackCooldown && currentState is HwaIdleState)
    {
        timer = 0f;
        float rand = Random.value;
        if (rand < 0.5f)
            ChangeState(new HwaFireStoneAttack());
        else
            ChangeState(new HwaEarthquakeAttack());
    }
}

    private void Start()
    {
        ChangeState(new HwaIdleState()); // 초기 상태: 대기
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

    /// <summary>
    /// 플레이어를 향해 이동합니다.
    /// </summary>
    public void MoveTowardsPlayer()
    {
        Vector2 direction = (player.localPosition - transform.localPosition).normalized;
        transform.localPosition += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.localPosition, attackRange); // 공격 범위 시각화
    }
}
