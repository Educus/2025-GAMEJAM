using UnityEngine;

public class CaptureBar : MonoBehaviour
{
    public Transform target;         
    public float offsetY = 1.5f;     

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 worldPosition = target.position + Vector3.down * offsetY;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        transform.position = screenPosition;
    }
}
