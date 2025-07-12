using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverImage; // UI 이미지 RectTransform
    [SerializeField] private float dropHeight = 800f; // 떨어지는 시작 높이 (화면 위)
    [SerializeField] private float dropDuration = 0.7f; // 떨어지는 시간
    [SerializeField] private float bounceHeight = 150f; // 튀어오르는 높이
    [SerializeField] private float bounceDuration = 0.3f; // 튀는 시간
    [SerializeField] private int bounceCount = 2; // 튀는 횟수

    private Vector2 groundPosition;

    private void Start()
    {
        // 화면 아래 바닥 위치 세팅 (이미지 원래 위치)
        groundPosition = gameOverImage.anchoredPosition;

        // 시작 위치를 화면 위로 이동
        gameOverImage.anchoredPosition = new Vector2(groundPosition.x, groundPosition.y + dropHeight);
    }

    public void ShowGameOver()
    {
        StartCoroutine(DropAndBounce());
    }

    private IEnumerator DropAndBounce()
    {
        // 떨어지는 애니메이션 (lerp)
        float elapsed = 0f;
        Vector2 startPos = gameOverImage.anchoredPosition;
        Vector2 endPos = groundPosition;

        while (elapsed < dropDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / dropDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease out (부드럽게 떨어지기)
            gameOverImage.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        gameOverImage.anchoredPosition = endPos;

        // 튀는 애니메이션 반복
        for (int i = 0; i < bounceCount; i++)
        {
            // 위로 튀기기
            elapsed = 0f;
            Vector2 bounceTop = endPos + Vector2.up * bounceHeight;
            while (elapsed < bounceDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / bounceDuration;
                t = Mathf.Sin(t * Mathf.PI * 0.5f); // ease out
                gameOverImage.anchoredPosition = Vector2.Lerp(endPos, bounceTop, t);
                yield return null;
            }
            gameOverImage.anchoredPosition = bounceTop;

            // 다시 떨어지기
            elapsed = 0f;
            while (elapsed < bounceDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / bounceDuration;
                t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f); // ease in
                gameOverImage.anchoredPosition = Vector2.Lerp(bounceTop, endPos, t);
                yield return null;
            }
            gameOverImage.anchoredPosition = endPos;

            // 튀어오르는 높이 줄이기 (반탄력 효과)
            bounceHeight *= 0.6f;
        }
    }
}