using System.Collections;
using System.Collections.Generic;
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

