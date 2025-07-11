using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IHitable
{
    // 최대체력
    [SerializeField] private int maxHp;
    private int hp;
    // 이동속도
    [SerializeField] private int speed;
    // 무적시간
    [SerializeField] private float invincibilityTime;
    private bool invincibility = false;
    private float invinTime;

    private Rigidbody2D rigid;
    private Vector2 moveDirection;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hp = maxHp;
        invinTime = invincibilityTime;
    }

    void Update()
    {
        Move();
        Invincibility();

    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    private void Invincibility()
    {
        if (invincibility)
        {
            invinTime -= Time.deltaTime;
            if (invinTime <= 0)
            {
                invincibility = false;
                invinTime = invincibilityTime; // 초기화
            }
        }
    }

    public void IHit(int damage)
    {
        if(invincibility)
        {
            return; // 무적 상태면 데미지 무시
        }

        hp -= damage;
        Debug.Log($"Player hit! Remaining HP: {hp}");

        invincibility = true; // 무적 상태로 전환
    }
}
