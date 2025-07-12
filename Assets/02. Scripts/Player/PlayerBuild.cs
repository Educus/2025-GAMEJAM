using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : Singleton<PlayerBuild>
{
    [SerializeField] private List<SoBuild> allList = new List<SoBuild>();

    [SerializeField] private List<SoBuild> buildList = new List<SoBuild>();
    private GameObject attackObj = null;

    private List<SoSkill> skillList = new List<SoSkill>();
    private List<SoStat> statList = new List<SoStat>();

    public int lv { get; private set; } = 0;
    private int maxExp = 100;
    private int nowExp = 0;

    public int maxHp { get; private set; } = 0;
    public int atk {get; private set;} = 0;
    public float range {get; private set;} = 0;
    public int atkCount {get; private set;} = 0;
    public float cooltime {get; private set;} = 0;
    public float moveSpeed {get; private set;} = 0;
    public float captureSpeed {get; private set;} = 0;
    public float invincibilityTime {get; private set;} = 0;

    public void AddBuild(SoBuild soBuild)
    {
        if (soBuild.mBuildType == BuildType.Skill)
        {
            if (!buildList.Contains(soBuild))
            {
                buildList.Add(soBuild);
                skillList.Add(soBuild.mBuildSkill);
                AddSkill(soBuild.mBuildSkill);
                Debug.Log("��ų ����");
            }
            else if(soBuild.mBuildSkill.isSkill3 == true) // �ñ��϶�?
            {
                SoBuild existingBuild = buildList.Find(b => b == soBuild);

                existingBuild.mBuildSkill.isSkill2 = true;
                existingBuild.mBuildSkill.isSkill3 = true;

            }
            else // �ߺ� ȹ���� ��
            {
                SoBuild existingBuild = buildList.Find(b => b == soBuild);

                if (existingBuild.mBuildSkill.isSkill1 == false)
                {
                    existingBuild.mBuildSkill.isSkill1 = true;
                }
                else if (existingBuild.mBuildSkill.isSkill2 == false)
                {
                    existingBuild.mBuildSkill.isSkill2 = true;
                }
            }
        }
        else if (soBuild.mBuildType == BuildType.Stat)
        {
            buildList.Add(soBuild);
            statList.Add(soBuild.mBuildStat);
            AddStat(soBuild.mBuildStat);
        }
    }
    private void AddSkill(SoSkill soSkill)
    {
        attackObj = GameManager.Instance.player.attackObj.gameObject;
        GameObject skill = Instantiate(soSkill.mSkillPrefab, attackObj.transform.position, Quaternion.identity);
        skill.transform.parent = attackObj.transform;
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

    public void AddExp(int value)
    {
        nowExp += value;

        if (nowExp > (maxExp + lv * 50))
        {
            nowExp -= (maxExp + lv * 50);
            LvUp();
        }
    }

    private void LvUp()
    {
        lv++;
    }

    [Tooltip("bool���� ���ɿ���, ���� ���ڴ� ���������� 3�� ���, ������ �� ������ŭ ���")]
    public List<SoBuild> GetRandomBuildChoices(bool isCaptured, int count = 3) 
    {
        List<SoBuild> selectable = new List<SoBuild>();

        foreach (var build in allList)
        {
            switch (build.mBuildType)
            {
                case BuildType.Stat:
                    // SoStat�� ���� ���� ����
                    selectable.Add(build);
                    break;

                case BuildType.Skill:
                    var skill = build.mBuildSkill;

                    if (skill == null) continue;

                    // 3�� �Ϸ� �� ����
                    if (skill.isSkill3) continue;

                    // 1�� ���
                    if (!skill.isSkill1)
                    {
                        selectable.Add(build);
                    }
                    // 2�� ���
                    else if (skill.isSkill1 && !skill.isSkill2)
                    {
                        selectable.Add(build);
                    }
                    // 3�� ��� (���� ������ ��쿡��)
                    else if (skill.isSkill1 && skill.isSkill2 && !skill.isSkill3 && isCaptured)
                    {
                        selectable.Add(build);
                    }

                    break;
            }
        }

        // ����
        for (int i = 0; i < selectable.Count; i++)
        {
            int rand = Random.Range(i, selectable.Count);
            (selectable[i], selectable[rand]) = (selectable[rand], selectable[i]);
        }

        // �ִ� count�� ����
        return selectable.GetRange(0, Mathf.Min(count, selectable.Count));
    }

    public void Reset()
    {
        buildList.Clear();
        statList.Clear();
        skillList.Clear();
    }
}
