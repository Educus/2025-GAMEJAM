using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "So/Skill")]
public class SoSkill : ScriptableObject
{
    [Header("스킬 공격력")]
    [SerializeField] private int atk;
    [Header("스킬 쿨타임")]
    [SerializeField] private float cooltime;

    public int mAtk => atk;
    public float mCooltime => cooltime;
}
