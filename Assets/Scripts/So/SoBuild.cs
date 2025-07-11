using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Build", menuName = "So/Build")]
public class SoBuild : ScriptableObject
{
    [Header("ºôµå Sprite")]
    [SerializeField] private Sprite buildSprite;
    [Header("ºôµå ÀÌ¸§")]
    [SerializeField] private string buildKorName;
    [Header("ºôµå ¼³¸í")]
    [SerializeField] private string buildDescription;
    [Header("ºôµå °ø°İ·Â")]
    [SerializeField] private int atk;
    [Header("ºôµå °ø°İ¼Óµµ")]
    [SerializeField] private float speed;

    public Sprite mBuildSprite => buildSprite;
    public string mBuildName => buildKorName;
    public int mAtk => atk;
    public float mSpeed => speed;
}
