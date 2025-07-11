using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int fmaxHp = 100;
    [SerializeField] private int fatk = 20;
    [SerializeField] private float frange = 5f;
    [SerializeField] private float fatkCount = 1f;
    [SerializeField] private float fcooltime = 1f;  // 100%
    [SerializeField] private float fmoveSpeed = 8f;
    [SerializeField] private float fcaptureSpeed = 1f;
    [SerializeField] private float finvincibilityTime = 0.5f;

    public int maxHp { get; private set; }
    public int hp { get; private set; }
    public int atk {get; private set;}
    public float range {get; private set;}
    public float atkCount {get; private set;}
    public float cooltime {get; private set;}
    public float moveSpeed {get; private set;}
    public float captureSpeed {get; private set;}
    public float invincibilityTime {get; private set; }

    private PlayerBuild build;
    private void Awake()
    {
        build = GetComponent<PlayerBuild>();
    }
    private void Start()
    {
        hp = fmaxHp;  // 초기 HP 설정
    }
    private void Update()
    {
        Hp();
        Stat();
    }

    private void Hp()
    {
        if(maxHp != fmaxHp + build.maxHp)
        {
            hp += fmaxHp + build.maxHp - maxHp;
            maxHp = fmaxHp + build.maxHp;
        }

        hp = Mathf.Clamp(hp, 0, maxHp); // HP가 최대치를 넘지 않도록 제한
    }
    private void Stat()
    {
        atk = fatk + build.atk;
        range = frange + build.range;
        atkCount = fatkCount + build.atkCount;
        cooltime = fcooltime + build.cooltime;
        moveSpeed = fmoveSpeed + build.moveSpeed;
        captureSpeed = fcaptureSpeed + build.captureSpeed;
        invincibilityTime = finvincibilityTime + build.invincibilityTime;
    }

    public void Damage(int value)
    {
        hp -= value;
    }

}
