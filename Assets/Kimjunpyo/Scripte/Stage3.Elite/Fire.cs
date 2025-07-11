using UnityEngine;

public class Fire : MonoBehaviour
{
    public float lifetime = 3f; // 화염의 지속 시간

    private void Start()
    {
        Destroy(gameObject, lifetime); // 일정 시간이 지나면 화염 제거
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.IHit(20); // 플레이어에게 데미지
            Destroy(gameObject); // 충돌 후 화염 제거
        }
    }
}