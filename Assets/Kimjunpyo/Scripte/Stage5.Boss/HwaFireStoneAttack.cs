using UnityEngine;
using System.Collections;

public class HwaFireStoneAttack : IHwaBossState
{
    private HwaBossController boss;

    public void OnEnter(HwaBossController boss)
    {
        this.boss = boss;
        Debug.Log("화염 돌 공격 시작");
        boss.StartCoroutine(LaunchStones());
    }

    IEnumerator LaunchStones()
    {
        yield return new WaitForSeconds(1f); // 시전 대기 시간

        for (int i = 0; i < 3; i++)
        {
            GameObject stone = GameObject.Instantiate(boss.fireStonePrefab, boss.transform.localPosition + Vector3.up, Quaternion.identity);
            stone.GetComponent<FireStone>().Initialize(boss.player, 1.5f, 5f);
            yield return new WaitForSeconds(0.5f); // 돌간 발사 간격
        }

        boss.ChangeState(new HwaIdleState()); // 대기 상태로 전환
    }

    public void OnUpdate() {}
    public void OnExit()
    {
        Debug.Log("화염 돌 공격 종료");
    }
}
