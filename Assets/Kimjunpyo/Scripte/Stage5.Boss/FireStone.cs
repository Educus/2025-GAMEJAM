using UnityEngine;

public class FireStone : MonoBehaviour
{
    [SerializeField] private float speed;     // 이동 속도
    [SerializeField] private float lifetime;  // 수명
    private float timer = 0f;
    private Transform target;                // 추적할 대상

    /// <summary>
    /// FireStone 초기화 메서드
    /// </summary>
    /// <param name="target">추적할 대상</param>
    /// <param name="speed">이동 속도</param>
    /// <param name="lifetime">수명</param>
    public void Initialize(Transform target, float speed, float lifetime)
    {
        this.target = target;
        this.speed = speed;
        this.lifetime = lifetime;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // 타겟이 없으면 화염 돌 제거
            return;
        }

        // 타겟 위치로 이동
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 수명 타이머
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject); // 수명이 다하면 화염 돌 제거
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("플레이어 충돌함");
        {
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.IHit(10);
            }
            Destroy(gameObject); 
        }
    }
}
