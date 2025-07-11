using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "So/Skill")]
public class SoSkill : ScriptableObject
{
    [Header("��ų ���ݷ�")]
    [SerializeField] private int atk;
    [Header("��ų ��Ÿ��")]
    [SerializeField] private float cooltime;

    public int mAtk => atk;
    public float mCooltime => cooltime;
}
