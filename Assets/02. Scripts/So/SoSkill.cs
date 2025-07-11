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
    [Header("스킬 공격력")]
    [SerializeField] private int atk;
    [Header("스킬 쿨타임")]
    [SerializeField] private float cooltime;

    [Header("스킬 공격 타입")]
    [SerializeField] private SkillType skillType;

    public int mAtk => atk;
    public float mCooltime => cooltime;
}
