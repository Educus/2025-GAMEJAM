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

    private Rigidbody2D rigid;
    private CapsuleCollider2D collider;

    private void Awake()
    {
        EnemyData();   
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        
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
    public void IHit(int damage)
    {
        throw new System.NotImplementedException();
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
