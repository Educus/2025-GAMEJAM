using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject puddlePrefab;
    [HideInInspector] public GameObject target;
    [HideInInspector] public int damage;
    [HideInInspector] public bool ultmit = false;
    [HideInInspector] public float range;

    private float time = 3f;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            ApplyAOEDamage(Vector2.zero);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ApplyAOEDamage(transform.position);
        }
    }

    private void ApplyAOEDamage(Vector2 center)
    {
        // 1. �ֺ� �ݶ��̴� ���� (2D Physics OverlapCircle ���)
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, range);

        foreach (Collider2D hit in hits)
        {
            // 2. "Enemy" �±� ���� Ȯ��
            if (hit.CompareTag("Enemy"))
            {
                // 3. IHitable �������̽� ����� ��쿡�� ����
                IHitable target = hit.GetComponent<IHitable>();
                if (target != null)
                {
                    target.IHit(damage);
                }
            }
        }

        if (ultmit)
        {
            GameObject puddle = Instantiate(puddlePrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}