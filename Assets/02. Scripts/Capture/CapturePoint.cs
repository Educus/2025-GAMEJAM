using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CapturePoint : MonoBehaviour
{
    [Header("점령 설정")]
    public Slider captureSlider;
    public float captureTime = 5f;
    public int maxEnemiesAllowed = 0;  // 이 수보다 많으면 점령 진행 불가

    private float captureTimer = 0f;
    private bool isCaptured = false;
    private bool isPlayerInside = false;

    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Update()
    {
        if (isCaptured) return;

        // 적이 많으면 점령 불가
        bool canCapture = enemiesInZone.Count <= maxEnemiesAllowed;

        if (isPlayerInside && canCapture)
        {
            captureTimer += Time.deltaTime * 0.3f; 
        }
        else
        {
            captureTimer -= Time.deltaTime * 0.2f; 
        }

        captureTimer = Mathf.Clamp(captureTimer, 0f, captureTime);
        float ratio = captureTimer / captureTime;

        if (captureSlider != null)
        {
            captureSlider.value = ratio;
            captureSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 1.5f);
        }

        if (ratio >= 1f)
        {
            isCaptured = true;
            Debug.Log("점령 완료!");
            // 점령 후 효과 넣고 싶으면 여기!

            BuildCardManager.Instance.ShowBuildChoicesUI();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }

        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInZone.Contains(other.gameObject))
            {
                enemiesInZone.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }

        if (other.CompareTag("Enemy"))
        {
            enemiesInZone.Remove(other.gameObject);
        }
    }
}
