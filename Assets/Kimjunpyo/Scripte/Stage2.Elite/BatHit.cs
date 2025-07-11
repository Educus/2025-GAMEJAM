using UnityEngine;

public class BatHit : MonoBehaviour
{
    private void Start()
    {
        // 경고 이펙트는 일정 시간이 지나면 자동으로 제거
        Destroy(gameObject, 0.5f);
    }
}