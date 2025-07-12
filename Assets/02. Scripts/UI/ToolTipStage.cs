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
        "���� ����",
        "����",
        "�����",
        "���� �����",
        "�ſ� �����\n���� ����"
    };

    public void ToolTips(int value)
    {
        Vector3 mousePos = Input.mousePosition;
        float screenCenterX = Screen.width / 2f;

        // ���콺�� ȭ�� �����ʿ� ������ ���ʿ� ǥ��
        bool showLeft = mousePos.x > screenCenterX;

        Vector3 offset = new Vector3(500f, 0, 0); // �ʿ�� ����
        tooltip.transform.position = mousePos + (showLeft ? -offset : offset);

        text.text = tooltip_text[value];

        tooltip.SetActive(true);
    }

    public void ToolTipsOff()
    {
        tooltip.SetActive(false);
    }

}
