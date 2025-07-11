using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "So/Stat")]
public class SoStat : ScriptableObject
{
    [Header("�߰����� Hp")]
    [SerializeField] private int hp;
    [Header("�߰����� ATK")]
    [SerializeField] private int atk;
    [Header("�߰����� ��Ÿ�� ����")]
    [SerializeField] private float cooltime;
    [Header("�߰����� ����ü ��Ÿ�")]
    [SerializeField] private float projectileRange;
    [Header("�߰����� ����ü ����")]
    [SerializeField] private int projectileCount;
    [Header("�߰����� �̵��ӵ�")]
    [SerializeField] private float moveSpeed;
    [Header("�߰����� ���ɼӵ�")]
    [SerializeField] private float captureSpeed;

    public int mHp => hp;
    public int mAtk => atk;
    public float mCooltime => cooltime;
    public float mProjectileRange => projectileRange;
    public int mProjectileCount => projectileCount;
    public float mMoveSpeed => moveSpeed;
    public float mCaptureSpeed => captureSpeed;

}
