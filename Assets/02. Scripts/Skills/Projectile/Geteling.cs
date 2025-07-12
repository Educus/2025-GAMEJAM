using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Geteling : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [SerializeField] private GameObject main;
    [HideInInspector] public GameObject parent;

    [HideInInspector] public int damage;
    [HideInInspector] public bool ultmit = false;

    private float value = 0.5f;
    private float time = 0;
    private bool hit = false;

    private void Update()
    {
        Move();
        SetTimer();
    }

    private void Move()
    {
        if (!target) return;

        Vector3 parentPos = parent.transform.position;

        // 1. y축으로 0.6 올림
        Vector3 basePos = parentPos + new Vector3(0f, 0.6f, 0f);

        // 2. 방향 계산 (target - base)
        Vector2 direction = (target.transform.position - basePos).normalized;

        // 3. 원하는 위치 = basePos + 방향 * 2f
        Vector2 finalPos = (Vector2)basePos + direction * 5f;

        // 4. 위치 이동
        main.transform.position = finalPos;

        // 5. 회전 방향 설정 (Z축 회전만 사용)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        main.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && hit)
        {
            Debug.Log("닿음");

            // 피해 주기
            collision.GetComponent<IHitable>().IHit(damage);

            // ultmit일 때만 밀기
            if (ultmit)
            {
                Debug.Log("밀침");

                Transform target = collision.transform;

                // 방향 계산 (parent → target)
                Vector2 direction = (target.position - (parent.transform.position + new Vector3(0f, 0.6f, 0f))).normalized;

                // Rigidbody2D가 있을 경우, MovePosition 사용 권장
                Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.MovePosition(rb.position + direction * 1f); // 1f = 밀치는 거리
                }
                else
                {
                    target.position += (Vector3)(direction * 1f);
                }
            }

            hit = false; // 타이머 초기화
        }
    }

    private void SetTimer()
    {
        time += Time.deltaTime;

        if (time > value)
        {
            time = 0;
            hit = true;
        }
    }
}
