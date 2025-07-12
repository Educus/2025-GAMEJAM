using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;         // ���� �÷��̾�
    [SerializeField] public Collider2D backgroundBounds; // ��� ���� (��: BoxCollider2D)

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

        // 1. �⺻ ī�޶� ��ġ�� �÷��̾� ����
        Vector3 targetPos = player.position;

        // 2. ī�޶� Z ��ġ�� ���� (2D��)
        targetPos.z = transform.position.z;

        // 3. ��� ��� ���ϱ�
        Bounds bounds = backgroundBounds.bounds;

        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        // 4. ��� ������ ����
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y + 1, minY, maxY);

        // 5. ���� ��ġ ����
        transform.position = new Vector3(clampedX, clampedY, targetPos.z);
    }
}
