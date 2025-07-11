using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ShortRange, // 근거리
    LongRange,  // 원거리
}
[CreateAssetMenu(fileName = "Skill", menuName = "So/Skill")]
public class SoSkill : ScriptableObject
{
    [Header("스킬 프리팹")]
    [SerializeField] private GameObject skillPrefab;
    [Header("스킬 공격력")]
    [SerializeField] private int atk;
    [Header("스킬 쿨타임")]
    [SerializeField] private float cooltime;

    [Header("스킬 공격 타입")]
    [SerializeField] private SkillType skillType;
    [Header("스킬 투사체 사거리")]
    [SerializeField] private float projectileDistance;
    [Header("스킬 투사체 속도")]
    [SerializeField] private float projectileSpeed;

    [Header("스킬 강화 1")]
    public bool isSkill1 = false;
    [Header("스킬 강화 2")]
    public bool isSkill2 = false;
    [Header("스킬 궁극")]
    public bool isSkill3 = false;

    public GameObject mSkillPrefab => skillPrefab;
    public int mAtk => atk;
    public float mCooltime => cooltime;
    public SkillType mSkillType => skillType;
    public float mProjectileDistance => projectileDistance;
    public float mProjectileSpeed => projectileSpeed;
}
