using UnityEngine;
using System.Collections;

public class HwaFireStoneAttack : IHwaBossState
{
    private HwaBossController boss;
    private Animator animator;

    public void OnEnter(HwaBossController boss)
    {
        this.boss = boss;
        animator = boss.GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator가 보스 오브젝트에 없습니다!");
            return;
        }

        animator.SetTrigger("Fire");
        Debug.Log("화염 돌 공격 시작");
        boss.StartCoroutine(LaunchStones());
    }

    private IEnumerator LaunchStones()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject stone = GameObject.Instantiate(
                boss.fireStonePrefab,
                boss.transform.position + Vector3.up,
                Quaternion.identity
            );

            if (boss.player != null)
            {
                stone.GetComponent<FireStone>().Initialize(boss.player, 5f, 10f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        boss.ChangeState(new HwaIdleState());
    }

    public void OnUpdate() { }

    public void OnExit()
    {
        if (animator != null)
            animator.SetTrigger("Move");
    }
}
