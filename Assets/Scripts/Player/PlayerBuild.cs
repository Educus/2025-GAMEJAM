using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    [SerializeField] private List<SoBuild> buildList = new List<SoBuild>();

    private List<SoSkill> skillList = new List<SoSkill>();
    private List<SoStat> statList = new List<SoStat>();

    public int maxHp = 0;
    public int atk = 0;
    public float range = 0;
    public int atkCount = 0;
    public float cooltime = 0;
    public float moveSpeed = 0;
    public float captureSpeed = 0;
    public float invincibilityTime = 0;
    public void AddBuild(SoBuild soBuild)
    {
        buildList.Add(soBuild);
        if (soBuild.mBuildType == BuildType.Skill)
        {
            skillList.Add(soBuild.mBuildSkill);
        }
        else if (soBuild.mBuildType == BuildType.Stat)
        {
            statList.Add(soBuild.mBuildStat);
            AddStat(soBuild.mBuildStat);
        }
    }
    private void AddStat(SoStat soStat)
    {
        maxHp += soStat.mHp;
        atk += soStat.mAtk;
        range += soStat.mProjectileRange;
        atkCount += soStat.mProjectileCount;
        cooltime += soStat.mCooltime;
        moveSpeed += soStat.mMoveSpeed;
        captureSpeed += soStat.mCaptureSpeed;
        invincibilityTime += soStat.mCaptureSpeed;
    }
}
