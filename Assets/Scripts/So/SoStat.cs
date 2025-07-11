using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "So/Stat")]
public class SoStat : ScriptableObject
{
    [Header("추가스탯 Hp")]
    [SerializeField] private int hp;
    [Header("추가스탯 ATK")]
    [SerializeField] private int atk;
    [Header("추가스탯 쿨타임 감소")]
    [SerializeField] private float cooltime;
    [Header("추가스탯 투사체 사거리")]
    [SerializeField] private float projectileRange;
    [Header("추가스탯 투사체 갯수")]
    [SerializeField] private int projectileCount;
    [Header("추가스탯 이동속도")]
    [SerializeField] private float moveSpeed;
    [Header("추가스탯 점령속도")]
    [SerializeField] private float captureSpeed;

    public int mHp => hp;
    public int mAtk => atk;
    public float mCooltime => cooltime;
    public float mProjectileRange => projectileRange;
    public int mProjectileCount => projectileCount;
    public float mMoveSpeed => moveSpeed;
    public float mCaptureSpeed => captureSpeed;

}
