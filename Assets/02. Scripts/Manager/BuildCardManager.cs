using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildCardManager : Singleton<BuildCardManager>
{
    [Header("UI 요소")]
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform cardPanel; // 카드 슬롯들이 들어갈 부모
    [SerializeField] private Button rerollButton;

    [Header("점령 여부")]
    [SerializeField] private bool isCaptured = false;

    private void Start()
    {
        rerollButton.onClick.AddListener(ShowBuildChoicesUI);
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 카드 3개 UI 생성
    /// </summary>
    public void ShowBuildChoicesUI()
    {
        obj.gameObject.SetActive(true);
        Time.timeScale = 0f;
        // 기존 카드 제거
        foreach (Transform child in cardPanel)
        {
            Destroy(child.gameObject);
        }

        List<SoBuild> choices = PlayerBuild.Instance.GetRandomBuildChoices(isCaptured, 3);

        foreach (SoBuild build in choices)
        {
            GameObject slot = Instantiate(cardSlotPrefab, cardPanel);

            // 카드 이미지 (CardSlot 자체의 Image 컴포넌트)
            Image image = slot.GetComponent<Image>();
            image.sprite = build.mBuildSprite;

            // 이름, 설명 TMP 텍스트
            TMP_Text nameText = slot.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text descText = slot.transform.Find("DescText").GetComponent<TMP_Text>();

            nameText.text = build.mBuildName;
            descText.text = build.mBuildDescription;

            // 버튼 연결
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => OnCardSelected(build));
        }
    }

    /// <summary>
    /// 카드 클릭 시 동작
    /// </summary>
    private void OnCardSelected(SoBuild selected)
    {
        PlayerBuild.Instance.AddBuild(selected);
        Time.timeScale = 1f;

        // UI 제거
        foreach (Transform child in cardPanel)
        {
            Destroy(child.gameObject);
        }

        obj.gameObject.SetActive(false);
        // TODO: 다음 단계로 넘어가는 처리 (예: 강화 완료 표시, 닫기 등)
    }
}
