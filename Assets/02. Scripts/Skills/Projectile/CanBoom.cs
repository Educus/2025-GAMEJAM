using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBoom : MonoBehaviour
{
    public float explosionRadius = 2.5f;
    public int explosionDamage = 20;
    public float autoExplodeTime = 4f;

    private void Start()
    {
        StartCoroutine(AutoExplode());
    }

    private IEnumerator AutoExplode()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 폭발 범위 감지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                IHitable target = hit.GetComponent<IHitable>();
                if (target != null)
                {
                    target.IHit(explosionDamage);
                }
            }
        }

        // 폭발 효과 (있다면)
        // Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
