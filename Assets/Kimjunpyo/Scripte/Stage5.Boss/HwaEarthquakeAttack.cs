using UnityEngine;
using System.Collections;

public class HwaEarthquakeAttack : IHwaBossState
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

        animator.SetTrigger("Earth");
        Debug.Log("지진 공격 시작");
        boss.StartCoroutine(DoEarthquake());
    }

    private IEnumerator DoEarthquake()
    {
        yield return new WaitForSeconds(1.5f); // 시전 대기 시간

        Collider2D[] targets = Physics2D.OverlapCircleAll(boss.transform.position, 8f);
        foreach (var col in targets)
        {
            if (col.CompareTag("Player"))
            {
                Player player = col.GetComponent<Player>();
                if (player != null)
                {
                    player.IHit(20);
                    player.Stun(1.0f);
                }
            }
        }

        boss.ChangeState(new HwaIdleState());
    }

    public void OnUpdate() { }

    public void OnExit()
    {
        if (animator != null)
        {
            animator.SetTrigger("Move"); // Earth → Move → Idle 전이용
        }
    }
}
