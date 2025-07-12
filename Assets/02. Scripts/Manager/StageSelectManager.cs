using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private Image[] images;

    private void Update()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (GameManager.Instance.clear[i] == true)
            {
                images[i].raycastTarget = false;
                images[i].color = new Color(0.3f, 0.3f, 0.3f);

                images[i].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }

    public void NextScenes(int value)
    {
        ScenesManager.Instance.LoadScene(value);
    }
}
