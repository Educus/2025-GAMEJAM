using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Build", menuName = "So/Build")]
public class SoBuild : ScriptableObject
{
    [Header("���� Sprite")]
    [SerializeField] private Sprite buildSprite;
    [Header("���� �̸�")]
    [SerializeField] private string buildKorName;
    [Header("���� ����")]
    [SerializeField] private string buildDescription;
    [Header("���� ���ݷ�")]
    [SerializeField] private int atk;
    [Header("���� ���ݼӵ�")]
    [SerializeField] private float speed;

    public Sprite mBuildSprite => buildSprite;
    public string mBuildName => buildKorName;
    public int mAtk => atk;
    public float mSpeed => speed;
}
