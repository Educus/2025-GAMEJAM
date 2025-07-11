using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IHitable
{
    // �ִ�ü��
    [SerializeField] private int maxHp;
    private int hp;
    // �̵��ӵ�
    [SerializeField] private int speed;
    // �����ð�
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
                invinTime = invincibilityTime; // �ʱ�ȭ
            }
        }
    }

    public void IHit(int damage)
    {
        if(invincibility)
        {
            return; // ���� ���¸� ������ ����
        }

        hp -= damage;
        Debug.Log($"Player hit! Remaining HP: {hp}");

        invincibility = true; // ���� ���·� ��ȯ
    }
}
