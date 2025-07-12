using UnityEngine;

public enum EnemyType
{
    ShortRange,
    LongRange,
}

[CreateAssetMenu(fileName = "Enemy", menuName = "So/Enemy")]
public class SoEnemy : ScriptableObject
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    public GameObject mEnemyPrefab => enemyPrefab;

    [Header("적 ID")]
    [SerializeField] private int enemyId;
    [Header("적 이름")]
    [SerializeField] private string enemyKorName;
    [Header("적 설명")]
    [SerializeField] private string enemyDescription;
    [Header("적 타입")]
    [SerializeField] private EnemyType enemyType;
    [Header("체력")]
    [SerializeField] private int hp;
    [Header("이동속도")]
    [SerializeField] private float moveSpeed;
    [Header("공격력")]
    [SerializeField] private int atk;
    [Header("공격 속도")]
    [SerializeField] private float atkSpeed;
    [Header("공격 사정거리")]
    [SerializeField] private float atkRange;
    [Header("경험치")]
    [SerializeField] private int exp;

    public int mEnemyId => enemyId;
    public string mEnemyName => enemyKorName;
    public string mEnemyDescription => enemyDescription;
    public EnemyType mEnemyType => enemyType;
    public int mHp => hp;
    public float mMoveSpeed => moveSpeed;
    public int mAtk => atk;
    public float mAtkSpeed => atkSpeed;
    public float mAtkRange => atkRange;
    public int mExp => exp;
}
