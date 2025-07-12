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
    private bool hasLaunchedEffectPlayed = false;

    private void Start()
    {
    
        }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            ApplyAOEDamage(transform.position);
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
        // 주변 범위 안의 적 탐지
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, range);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                IHitable target = hit.GetComponent<IHitable>();
                if (target != null)
                {
                    target.IHit(damage);
                }
            }
        }

        // 궁극기 버전이라면 웅덩이 생성
        if (ultmit && puddlePrefab != null)
        {
            Instantiate(puddlePrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
