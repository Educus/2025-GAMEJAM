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
    [Header("��ų ���ݷ�")]
    [SerializeField] private int atk;
    [Header("��ų ��Ÿ��")]
    [SerializeField] private float cooltime;

    [Header("��ų ���� Ÿ��")]
    [SerializeField] private SkillType skillType;

    public int mAtk => atk;
    public float mCooltime => cooltime;
}
