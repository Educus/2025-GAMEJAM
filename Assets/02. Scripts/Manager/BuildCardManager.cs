using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildCardManager : Singleton<BuildCardManager>
{
    [Header("UI ���")]
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform cardPanel; // ī�� ���Ե��� �� �θ�
    [SerializeField] private Button rerollButton;

    [Header("���� ����")]
    [SerializeField] private bool isCaptured = false;

    private void Start()
    {
        rerollButton.onClick.AddListener(ShowBuildChoicesUI);
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// ī�� 3�� UI ����
    /// </summary>
    public void ShowBuildChoicesUI()
    {
        obj.gameObject.SetActive(true);
        Time.timeScale = 0f;
        // ���� ī�� ����
        foreach (Transform child in cardPanel)
        {
            Destroy(child.gameObject);
        }

        List<SoBuild> choices = PlayerBuild.Instance.GetRandomBuildChoices(isCaptured, 3);

        foreach (SoBuild build in choices)
        {
            GameObject slot = Instantiate(cardSlotPrefab, cardPanel);

            // ī�� �̹��� (CardSlot ��ü�� Image ������Ʈ)
            Image image = slot.GetComponent<Image>();
            image.sprite = build.mBuildSprite;

            // �̸�, ���� TMP �ؽ�Ʈ
            TMP_Text nameText = slot.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text descText = slot.transform.Find("DescText").GetComponent<TMP_Text>();

            nameText.text = build.mBuildName;
            descText.text = build.mBuildDescription;

            // ��ư ����
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => OnCardSelected(build));
        }
    }

    /// <summary>
    /// ī�� Ŭ�� �� ����
    /// </summary>
    private void OnCardSelected(SoBuild selected)
    {
        PlayerBuild.Instance.AddBuild(selected);
        Time.timeScale = 1f;

        // UI ����
        foreach (Transform child in cardPanel)
        {
            Destroy(child.gameObject);
        }

        obj.gameObject.SetActive(false);
        // TODO: ���� �ܰ�� �Ѿ�� ó�� (��: ��ȭ �Ϸ� ǥ��, �ݱ� ��)
    }
}
