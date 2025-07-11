using UnityEngine;

public class IHwaWarningFiller : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float duration;
    private float timer = 0f;

    public void Initialize(LineRenderer lineRenderer, float duration)
    {
        this.lineRenderer = lineRenderer;
        this.duration = duration;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / duration);

        // 그라디언트의 알파 값을 점점 증가
        Gradient gradient = lineRenderer.colorGradient;
        GradientAlphaKey[] alphaKeys = gradient.alphaKeys;
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = progress;
        }
        gradient.alphaKeys = alphaKeys;
        lineRenderer.colorGradient = gradient;

        if (progress >= 1f)
        {
            Destroy(this); // 채우기 완료 후 스크립트 제거
        }
    }
}