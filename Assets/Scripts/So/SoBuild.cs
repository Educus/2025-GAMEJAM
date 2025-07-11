using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType
{
    Skill,  // ½ºÅ³
    Stat,   // ½ºÅÈ
}

[CreateAssetMenu(fileName = "Build", menuName = "So/Build")]
public class SoBuild : ScriptableObject
{
    [Header("ºôµå Sprite")]
    [SerializeField] private Sprite buildSprite;
    [Header("ºôµå ÀÌ¸§")]
    [SerializeField] private string buildKorName;
    [Header("ºôµå ¼³¸í")]
    [SerializeField] private string buildDescription;
    [Header("ºôµå Å¸ÀÔ")]
    [SerializeField] private BuildType buildType;
    [Header("ºôµå ¾÷±×·¹ÀÌµå")]
    [SerializeField] private SoBuild upgradeBuild;

    [Header("ºôµå ½ºÅÈ")]
    [SerializeField] private SoStat buildStat;
    [Header("ºôµå ½ºÅ³")]
    [SerializeField] private SoSkill buildSkill;

    public Sprite mBuildSprite => buildSprite;
    public string mBuildName => buildKorName;
    public string mBuildDescription => buildDescription;
    public BuildType mBuildType => buildType;
    public SoBuild mUpgradeBuild => upgradeBuild;

    public SoStat mBuildStat => buildStat;
    public SoSkill mBuildSkill => buildSkill;
}
