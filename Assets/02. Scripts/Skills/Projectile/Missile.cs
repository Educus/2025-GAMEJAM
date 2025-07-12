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
        // 1. 주변 콜라이더 감지 (2D Physics OverlapCircle 사용)
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, range);

        foreach (Collider2D hit in hits)
        {
            // 2. "Enemy" 태그 여부 확인
            if (hit.CompareTag("Enemy"))
            {
                // 3. IHitable 인터페이스 적용된 경우에만 피해
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