using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ShortRange, // �ٰŸ�
    LongRange,  // ���Ÿ�
}
[CreateAssetMenu(fileName = "Skill", menuName = "So/Skill")]
public class SoSkill : ScriptableObject
{
    [Header("��ų ������")]
    [SerializeField] private GameObject skillPrefab;
    [Header("��ų ���ݷ�")]
    [SerializeField] private int atk;
    [Header("��ų ��Ÿ��")]
    [SerializeField] private float cooltime;

    [Header("��ų ���� Ÿ��")]
    [SerializeField] private SkillType skillType;
    [Header("��ų ����ü ��Ÿ�")]
    [SerializeField] private float projectileDistance;
    [Header("��ų ����ü �ӵ�")]
    [SerializeField] private float projectileSpeed;

    [Header("��ų ��ȭ 1")]
    public bool isSkill1 = false;
    [Header("��ų ��ȭ 2")]
    public bool isSkill2 = false;
    [Header("��ų �ñ�")]
    public bool isSkill3 = false;

    public GameObject mSkillPrefab => skillPrefab;
    public int mAtk => atk;
    public float mCooltime => cooltime;
    public SkillType mSkillType => skillType;
    public float mProjectileDistance => projectileDistance;
    public float mProjectileSpeed => projectileSpeed;
}
