using UnityEngine;

public class FireStone : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float lifetime;
    private float timer = 0f;

    public void Initialize(Transform target, float speed, float lifetime)
    {
        this.target = target;
        this.speed = speed;
        this.lifetime = lifetime;
    }

    void Update()
    {
        if (target == null) return;

        // 2D 이동 처리
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.IHit(10);
            Destroy(gameObject);
        }
    }
}
