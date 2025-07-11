using UnityEngine;

public class Slash : MonoBehaviour
{
    private Vector3 targetPosition;
    private float holdDuration = 1.5f; // 참격 유지 시간
    private bool isFlying = false; // 참격이 날아가는 상태인지 확인

    public void Initialize(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        if (!isFlying)
        {
            // 참격 유지 시간 동안 대기
            holdDuration -= Time.deltaTime;
            if (holdDuration <= 0f)
            {
                isFlying = true; // 참격이 날아가기 시작
            }
        }
        else
        {
            // 참격이 플레이어를 향해 날아감
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);

            // 목표 위치에 도달하면 제거
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.IHit(20); // 플레이어에게 데미지
            Destroy(gameObject); // 충돌 후 참격 제거
        }
    }
}