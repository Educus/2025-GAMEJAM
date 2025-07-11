using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    [SerializeField] private List<SoBuild> buildList = new List<SoBuild>();
    [SerializeField] GameObject attackObj;

    private List<SoSkill> skillList = new List<SoSkill>();
    private List<SoStat> statList = new List<SoStat>();

    public int maxHp { get; private set; } = 0;
    public int atk {get; private set;} = 0;
    public float range {get; private set;} = 0;
    public int atkCount {get; private set;} = 0;
    public float cooltime {get; private set;} = 0;
    public float moveSpeed {get; private set;} = 0;
    public float captureSpeed {get; private set;} = 0;
    public float invincibilityTime {get; private set;} = 0;

    private void Start()
    {
        AddSkill(buildList[0].mBuildSkill);
    }
    public void AddBuild(SoBuild soBuild)
    {
        buildList.Add(soBuild);
        if (soBuild.mBuildType == BuildType.Skill)
        {
            skillList.Add(soBuild.mBuildSkill);
            AddSkill(soBuild.mBuildSkill);
        }
        else if (soBuild.mBuildType == BuildType.Stat)
        {
            statList.Add(soBuild.mBuildStat);
            AddStat(soBuild.mBuildStat);
        }
    }
    private void AddSkill(SoSkill soSkill)
    {
        // ��ȭ ��ų �߰��� ���� ��ų�� �ϳ��� ��� �ϳ� �� �߰�
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
