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

        // 1. y������ 0.6 �ø�
        Vector3 basePos = parentPos + new Vector3(0f, 0.6f, 0f);

        // 2. ���� ��� (target - base)
        Vector2 direction = (target.transform.position - basePos).normalized;

        // 3. ���ϴ� ��ġ = basePos + ���� * 2f
        Vector2 finalPos = (Vector2)basePos + direction * 5f;

        // 4. ��ġ �̵�
        main.transform.position = finalPos;

        // 5. ȸ�� ���� ���� (Z�� ȸ���� ���)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        main.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && hit)
        {
            Debug.Log("����");

            // ���� �ֱ�
            collision.GetComponent<IHitable>().IHit(damage);

            // ultmit�� ���� �б�
            if (ultmit)
            {
                Debug.Log("��ħ");

                Transform target = collision.transform;

                // ���� ��� (parent �� target)
                Vector2 direction = (target.position - (parent.transform.position + new Vector3(0f, 0.6f, 0f))).normalized;

                // Rigidbody2D�� ���� ���, MovePosition ��� ����
                Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.MovePosition(rb.position + direction * 1f); // 1f = ��ġ�� �Ÿ�
                }
                else
                {
                    target.position += (Vector3)(direction * 1f);
                }
            }

            hit = false; // Ÿ�̸� �ʱ�ȭ
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
