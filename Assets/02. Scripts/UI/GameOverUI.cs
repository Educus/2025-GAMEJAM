using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverImage; // UI �̹��� RectTransform
    [SerializeField] private float dropHeight = 800f; // �������� ���� ���� (ȭ�� ��)
    [SerializeField] private float dropDuration = 0.7f; // �������� �ð�
    [SerializeField] private float bounceHeight = 150f; // Ƣ������� ����
    [SerializeField] private float bounceDuration = 0.3f; // Ƣ�� �ð�
    [SerializeField] private int bounceCount = 2; // Ƣ�� Ƚ��

    private Vector2 groundPosition;

    private void Start()
    {
        // ȭ�� �Ʒ� �ٴ� ��ġ ���� (�̹��� ���� ��ġ)
        groundPosition = gameOverImage.anchoredPosition;

        // ���� ��ġ�� ȭ�� ���� �̵�
        gameOverImage.anchoredPosition = new Vector2(groundPosition.x, groundPosition.y + dropHeight);
    }

    public void ShowGameOver()
    {
        StartCoroutine(DropAndBounce());
    }

    private IEnumerator DropAndBounce()
    {
        // �������� �ִϸ��̼� (lerp)
        float elapsed = 0f;
        Vector2 startPos = gameOverImage.anchoredPosition;
        Vector2 endPos = groundPosition;

        while (elapsed < dropDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / dropDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease out (�ε巴�� ��������)
            gameOverImage.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        gameOverImage.anchoredPosition = endPos;

        // Ƣ�� �ִϸ��̼� �ݺ�
        for (int i = 0; i < bounceCount; i++)
        {
            // ���� Ƣ���
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

            // �ٽ� ��������
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

            // Ƣ������� ���� ���̱� (��ź�� ȿ��)
            bounceHeight *= 0.6f;
        }
    }
}