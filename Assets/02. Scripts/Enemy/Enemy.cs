using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] SoEnemy enemyData;

    private Sprite enemySprite;
    private string enemyKorName;
    private string enemyDescription;
    private int maxHp;
    private int atk;
    private float speed;

    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private CapsuleCollider2D collider;

    private GameObject target;

    private int hp;

    private void Awake()
    {
        EnemyData();   
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();

        hp = maxHp;
    }


    void Update()
    {
        FindPlayer();
        MoveToTarget();
    }

    private void EnemyData()
    {
        if (enemyData != null)
        {
            enemySprite = enemyData.mEnemySprite;
            enemyKorName = enemyData.mEnemyName;
            enemyDescription = enemyData.mEnemyDescription;
            maxHp = enemyData.mHp;
            atk = enemyData.mAtk;
            speed = enemyData.mMoveSpeed;
        }
        else
        {
            Debug.LogError("Enemy data is not assigned!");
        }
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
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rigid.velocity = direction * speed;

        if (direction.x > 0)
        {
            sprite.flipX = false; // 오른쪽을 바라보도록
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true; // 왼쪽을 바라보도록
        }
    }

    public void IHit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        // 경험치 오브 드랍
        // GameManager.Instance.player.GetComponent<Player>().GainExp(enemyData.mExp);
        // 몬스터 제거
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();

            if (hitable != null)
            {
                hitable.IHit(atk);
            }
        }
    }
}
