using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;         // 따라갈 플레이어
    [SerializeField] public Collider2D backgroundBounds; // 배경 도형 (예: BoxCollider2D)

    private Camera camera;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        player = GameManager.Instance.player.transform;
        camera = GetComponent<Camera>();
        cameraHalfHeight = camera.orthographicSize;
        cameraHalfWidth = camera.aspect * cameraHalfHeight;
    }

    private void LateUpdate()
    {
        if (player == null || backgroundBounds == null) return;

        // 1. 기본 카메라 위치는 플레이어 기준
        Vector3 targetPos = player.position;

        // 2. 카메라 Z 위치는 고정 (2D용)
        targetPos.z = transform.position.z;

        // 3. 배경 경계 구하기
        Bounds bounds = backgroundBounds.bounds;

        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        // 4. 경계 안으로 제한
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y + 1, minY, maxY);

        // 5. 최종 위치 적용
        transform.position = new Vector3(clampedX, clampedY, targetPos.z);
    }
}
