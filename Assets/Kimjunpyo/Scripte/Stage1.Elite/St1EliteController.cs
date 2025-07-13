using UnityEngine;
using UnityEngine.Animations;

public class St1EliteController : MonoBehaviour, IHitable
{
    [SerializeField] int hp = 500;

    public Transform player; // 플레이어 위치
    public GameObject rockPrefab; // '락이다' 프리팹
    public GameObject warningEffectPrefab; // 경고 이펙트 프리팹
    public GameObject impactEffectPrefab; // 충돌 이펙트 프리팹
    public float attackCooldown = 7f; // 스킬 주기
    public float attackRange = 10f; // 공격 범위
    public float moveSpeed = 2f; // 이동 속도
    public float followSpeed = 5f;
    private float timer = 0f; // 쿨타임 타이머
    public Animator animator;

    private St1EliteState currentState; // 현재 상태

    private void Awake()
{
    if (animator == null)
    {
        animator = GetComponent<Animator>();
    }
}


    private void Start()
    {
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

        ChangeState(new St1IdleState()); // 초기 상태: 대기
    }

    private void Update()
    {
        timer += Time.deltaTime;
        currentState?.OnUpdate();

        // 상태 전환은 각 상태에서 관리되므로 여기서는 추가 로직을 두지 않음
    }

    public void ChangeState(St1EliteState newState)
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

    public void IHit(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
