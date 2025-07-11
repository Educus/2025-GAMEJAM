using UnityEngine;

public class Rock : MonoBehaviour
{
    private Vector3 targetPosition; // 락이 떨어질 위치
    private GameObject impactEffectPrefab; // 충돌 시 생성될 이펙트 프리팹

    public void Initialize(Vector3 targetPosition, GameObject impactEffectPrefab)
    {
        this.targetPosition = targetPosition;
        this.impactEffectPrefab = impactEffectPrefab;
    }

    private void Update()
    {
        // 락이 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);

        // 목표 위치에 도달하면 충돌 이펙트 생성 후 제거
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            CreateImpactEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.IHit(20); // 플레이어에게 데미지
            CreateImpactEffect();
            Destroy(gameObject); // 충돌 후 락 제거
        }
    }

    private void CreateImpactEffect()
    {
        if (impactEffectPrefab != null)
        {
            GameObject impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(impactEffect, 1f); // 이펙트 제거
        }
    }
}