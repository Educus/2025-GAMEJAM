using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour, IHitable
{
    private SoEnemy enemyData;

    private string enemyKorName;
    private string enemyDescription;
    private int maxHp;
    private int atk;
    private float speed;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private CapsuleCollider2D enemycollider;

    private GameObject target;

    private int hp;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        enemycollider = GetComponent<CapsuleCollider2D>();
        hp = maxHp;
    }

    void Update()
    {
        FindPlayer();
        MoveToTarget();
    }

    public void SetData(SoEnemy data, float hpMultiplier, float atkMultiplier)
    {
        if (data == null)
        {
            Debug.LogError("Enemy SO is null!");
            return;
        }

        enemyData = data;

        enemyKorName = data.mEnemyName;
        enemyDescription = data.mEnemyDescription;
        maxHp = Mathf.RoundToInt(data.mHp * hpMultiplier);
        atk = Mathf.RoundToInt(data.mAtk * atkMultiplier);
        speed = data.mMoveSpeed;

        hp = maxHp;

        // 스프라이트는 프리팹에 설정된 SpriteRenderer 기준
        // 단, 필요시 sprite.sprite = ... 으로 교체 가능
    }

    private void FindPlayer()
    {
        if (target == null)
        {
            if (GameManager.Instance.player == null) return;
            target = GameManager.Instance.player.gameObject;
        }
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rigid.velocity = direction * speed;

        if (direction.x > 0)
            sprite.flipX = false;
        else if (direction.x < 0)
            sprite.flipX = true;
    }

    public void IHit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
            Dead();
    }

    private void Dead()
    {
        // GameManager.Instance.player.GetComponent<Player>().GainExp(enemyData.mExp);
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();
            if (hitable != null)
                hitable.IHit(atk);
        }
    }
}
