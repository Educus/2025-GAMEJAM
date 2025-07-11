using UnityEngine;
using System.Collections;

/// <summary>
/// 보스의 지진 공격 상태.
/// </summary>
public class HwaEarthquakeAttack : IHwaBossState
{
    private HwaBossController boss;

    public void OnEnter(HwaBossController boss)
    {
        this.boss = boss;
        Debug.Log("지진 공격 시작");
        boss.StartCoroutine(DoEarthquake());
    }

    IEnumerator DoEarthquake()
    {
        // 경고 이펙트 표시
        GameObject warn = GameObject.Instantiate(boss.warningFanFX, boss.transform.localPosition, Quaternion.identity);
        warn.transform.LookAt(boss.player);
        GameObject.Destroy(warn, 1.5f);

        yield return new WaitForSeconds(1.5f); // 시전 대기 시간

        // 공격 판정
        Collider2D[] targets = Physics2D.OverlapCircleAll(boss.transform.localPosition, 8f);
        foreach (var col in targets)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().IHit(20); // 데미지
                col.GetComponent<Player>().Stun(1.0f); // 1초 기절
            }
        }

        boss.ChangeState(new HwaIdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}
    public void OnExit()
    {
        Debug.Log("지진 공격 종료");
    }
}
