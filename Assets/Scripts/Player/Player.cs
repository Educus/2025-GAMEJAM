using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IHitable
{
    [SerializeField] private Slider hpBar;
    private bool invincibility = false;
    private float invinTime;

    private PlayerStat playerStat;
    private Rigidbody2D rigid;
    private Vector2 moveDirection;
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        rigid = GetComponent<Rigidbody2D>();
        invinTime = playerStat.invincibilityTime;
    }

    void Update()
    {
        Move();
        Invincibility();
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveDirection * playerStat.moveSpeed * Time.fixedDeltaTime);
        HpBar();
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
                invinTime = playerStat.invincibilityTime; // 초기화
            }
        }
    }

    private void HpBar()
    {
        if (hpBar != null)
        {
            hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 3.5f);
            hpBar.value = (float)playerStat.hp / playerStat.maxHp;
        }
    }

    public void IHit(int damage)
    {
        if(invincibility)
        {
            return; // 무적 상태면 데미지 무시
        }

        playerStat.Damage(damage);
        Debug.Log($"Player hit! Remaining HP: {playerStat.hp}");

        invincibility = true; // 무적 상태로 전환
    }
}
