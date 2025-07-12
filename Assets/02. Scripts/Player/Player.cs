using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IHitable
{
    [SerializeField] private Slider hpBar;
    [SerializeField] public Transform attackObj;
    [SerializeField] private SoBuild firstSkill;
    private bool invincibility = false;
    private float invinTime;

    public PlayerStat playerStat;
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private Animator anim;
    private Vector2 moveDirection;
    void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        invinTime = playerStat.invincibilityTime;

        PlayerBuild.Instance.AddBuild(firstSkill);
    }

    void Update()
    {
        Move();
        Invincibility();
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveDirection * playerStat.moveSpeed * Time.fixedDeltaTime);
        anim.SetInteger("Speed", (int)playerStat.moveSpeed);
        HpBar();
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if(moveX > 0)
        {
            sprite.flipX = true;
            attackObj.localPosition = new Vector3(Mathf.Abs(attackObj.localPosition.x), attackObj.localPosition.y);

        }
        else if(moveX < 0)
        {
            sprite.flipX = false;
            attackObj.localPosition = new Vector3(-Mathf.Abs(attackObj.localPosition.x), attackObj.localPosition.y);
        }

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
                invinTime = playerStat.invincibilityTime; // �ʱ�ȭ
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
        if (invincibility)
        {
            return; // ���� ���¸� ������ ����
        }

        playerStat.Damage(damage);
        Debug.Log($"Player hit! Remaining HP: {playerStat.hp}");

        invincibility = true; // ���� ���·� ��ȯ
    }

     public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        moveDirection = Vector2.zero;
        enabled = false; 

        yield return new WaitForSeconds(duration);

        enabled = true;
    }
}

