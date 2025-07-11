using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ShortRange, // 근거리
    LongRange,  // 원거리
}

[CreateAssetMenu(fileName = "Enemy", menuName = "So/Enemy")]
public class SoEnemy : ScriptableObject
{
    [Header("몬스터 Sprite")]
    [SerializeField] private Sprite enemySprite;
    [Header("몬스터 ID")]
    [SerializeField] private int enemyId;
    [Header("몬스터 이름")]
    [SerializeField] private string enemyKorName;
    [Header("몬스터 설명")]
    [SerializeField] private string enemyDescription;
    [Header("몬스터 타입")]
    [SerializeField] private EnemyType enemyType;
    [Header("몬스터 체력")]
    [SerializeField] private int hp;
    [Header("몬스터 이동속도")]
    [SerializeField] private float moveSpeed;
    [Header("몬스터 공격력")]
    [SerializeField] private int atk;
    [Header("몬스터 공격 속도")]
    [SerializeField] private float atkSpeed;
    [Header("몬스터 공격 사거리")]
    [SerializeField] private float atkRange;
    [Header("몬스터 경험치")]
    [SerializeField] private int exp;

    public Sprite mEnemySprite => enemySprite;
    public int mEnemyId => enemyId;
    public string mEnemyName => enemyKorName;
    public string mEnemyDescription => enemyDescription;
    public int mHp => hp;
    public float mMoveSpeed => moveSpeed;
    public int mAtk => atk;
    public float mAtkSpeed => atkSpeed;
    public float mAtkRange => atkRange;
    public int mExp => exp;
}
