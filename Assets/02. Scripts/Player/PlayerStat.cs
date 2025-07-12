using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int fmaxHp = 100;
    [SerializeField] private int fatk = 20;                     // 공격력
    [SerializeField] private float frange = 5f;                 // 사거리
    [SerializeField] private float fatkCount = 0f;              // 투사체 갯수
    [SerializeField] private float fcooltime = 0f;              // 쿨타임 감소되는 시간
    [SerializeField] private float fmoveSpeed = 8f;             // 이동속도
    [SerializeField] private float fcaptureSpeed = 1f;          // 점령시간
    [SerializeField] private float finvincibilityTime = 0.5f;   // 무적시간

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
        build = PlayerBuild.Instance.GetComponent<PlayerBuild>();
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
