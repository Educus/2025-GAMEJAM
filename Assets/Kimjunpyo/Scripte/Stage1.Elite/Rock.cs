using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 10f;
    private GameObject impactEffectPrefab;

    public void Initialize(GameObject impactEffectPrefab)
    {
        this.impactEffectPrefab = impactEffectPrefab;
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -20f)
        {
            CreateImpactEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.IHit(20); // 플레이어 피해
            CreateImpactEffect();
            Destroy(gameObject);
        }
    }

    private void CreateImpactEffect()
    {
        if (impactEffectPrefab != null)
        {
            GameObject effect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
