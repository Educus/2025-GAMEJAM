using UnityEngine;

public class CatDongDong : MonoBehaviour
{
    public float speed = 100f; // 이동 속도
    public float passiveRotationSpeed = 30f; // 날아가는 도중 회전 속도

    private RectTransform canvasRect; // Canvas의 RectTransform
    private RectTransform rectTransform; // CatDongDong의 RectTransform
    private Vector2 direction; // 이동 방향

    private float currentZRotation = 0f; // 현재 회전 각도
    private float targetZRotation = 0f;  // 목표 회전 각도

    void Start()
    {
        // RectTransform 가져오기
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // 시작 위치를 Canvas의 정중앙으로 고정
        rectTransform.localPosition = Vector3.zero;

        // 초기 이동 방향을 랜덤하게 설정
        direction = Random.insideUnitCircle.normalized;

        // 초기 회전값 설정
        currentZRotation = 0f;
        targetZRotation = 0f;
    }

    void Update()
    {
        // 이동
        rectTransform.localPosition += (Vector3)(direction * speed * Time.deltaTime);

        // 자연 회전 (날아가는 동안 회전 추가)
        targetZRotation += passiveRotationSpeed * Time.deltaTime;

        // 회전 부드럽게 보간 적용
        currentZRotation = Mathf.LerpAngle(currentZRotation, targetZRotation, Time.deltaTime * 5f);
        rectTransform.localEulerAngles = new Vector3(0, 0, currentZRotation);

        // Canvas 경계 감지
        Vector3 position = rectTransform.localPosition;
        Vector2 halfSize = rectTransform.sizeDelta / 2f;
        Vector2 canvasHalfSize = canvasRect.sizeDelta / 2f;

        // X축 경계 체크
        if (position.x - halfSize.x < -canvasHalfSize.x || position.x + halfSize.x > canvasHalfSize.x)
        {
            direction.x = -direction.x; // X축 반전
            position.x = Mathf.Clamp(position.x, -canvasHalfSize.x + halfSize.x, canvasHalfSize.x - halfSize.x);

            targetZRotation += Random.Range(30f, 60f); // 반동 회전
        }

        // Y축 경계 체크
        if (position.y - halfSize.y < -canvasHalfSize.y || position.y + halfSize.y > canvasHalfSize.y)
        {
            direction.y = -direction.y; // Y축 반전
            position.y = Mathf.Clamp(position.y, -canvasHalfSize.y + halfSize.y, canvasHalfSize.y - halfSize.y);

            targetZRotation += Random.Range(30f, 60f); // 반동 회전
        }

        // 위치 반영
        rectTransform.localPosition = position;
    }
}
