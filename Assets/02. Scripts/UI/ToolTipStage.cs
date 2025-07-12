using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipStage : MonoBehaviour
{
    [SerializeField] GameObject tooltip;
    [SerializeField] TMP_Text text;

    private string[] tooltip_text = 
    { 
        "가장 쉬움",
        "보통",
        "어려움",
        "많이 어려움",
        "매우 어려움\n보스 주의"
    };

    public void ToolTips(int value)
    {
        Vector3 mousePos = Input.mousePosition;
        float screenCenterX = Screen.width / 2f;

        // 마우스가 화면 오른쪽에 있으면 왼쪽에 표시
        bool showLeft = mousePos.x > screenCenterX;

        Vector3 offset = new Vector3(500f, 0, 0); // 필요시 조정
        tooltip.transform.position = mousePos + (showLeft ? -offset : offset);

        text.text = tooltip_text[value];

        tooltip.SetActive(true);
    }

    public void ToolTipsOff()
    {
        tooltip.SetActive(false);
    }

}
