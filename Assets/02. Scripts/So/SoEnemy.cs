using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ShortRange, // �ٰŸ�
    LongRange,  // ���Ÿ�
}

[CreateAssetMenu(fileName = "Enemy", menuName = "So/Enemy")]
public class SoEnemy : ScriptableObject
{
    [Header("���� Sprite")]
    [SerializeField] private Sprite enemySprite;
    [Header("���� ID")]
    [SerializeField] private int enemyId;
    [Header("���� �̸�")]
    [SerializeField] private string enemyKorName;
    [Header("���� ����")]
    [SerializeField] private string enemyDescription;
    [Header("���� Ÿ��")]
    [SerializeField] private EnemyType enemyType;
    [Header("���� ü��")]
    [SerializeField] private int hp;
    [Header("���� �̵��ӵ�")]
    [SerializeField] private float moveSpeed;
    [Header("���� ���ݷ�")]
    [SerializeField] private int atk;
    [Header("���� ���� �ӵ�")]
    [SerializeField] private float atkSpeed;
    [Header("���� ���� ��Ÿ�")]
    [SerializeField] private float atkRange;
    [Header("���� ����ġ")]
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
