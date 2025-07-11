using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType
{
    Skill,  // ��ų
    Stat,   // ����
}

[CreateAssetMenu(fileName = "Build", menuName = "So/Build")]
public class SoBuild : ScriptableObject
{
    [Header("���� Sprite")]
    [SerializeField] private Sprite buildSprite;
    [Header("���� �̸�")]
    [SerializeField] private string buildKorName;
    [Header("���� ����")]
    [SerializeField] private string buildDescription;
    [Header("���� Ÿ��")]
    [SerializeField] private BuildType buildType;
    [Header("���� ���׷��̵�")]
    [SerializeField] private SoBuild upgradeBuild;

    [Header("���� ����")]
    [SerializeField] private SoStat buildStat;
    [Header("���� ��ų")]
    [SerializeField] private SoSkill buildSkill;

    public Sprite mBuildSprite => buildSprite;
    public string mBuildName => buildKorName;
    public string mBuildDescription => buildDescription;
    public BuildType mBuildType => buildType;
    public SoBuild mUpgradeBuild => upgradeBuild;

    public SoStat mBuildStat => buildStat;
    public SoSkill mBuildSkill => buildSkill;
}
