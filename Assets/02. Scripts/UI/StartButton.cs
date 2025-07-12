using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource hoverAudio;  // 마우스 오버 시 재생할 소리

    public void StageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverAudio != null && !hoverAudio.isPlaying)
        {
            hoverAudio.Play();
        }
    }
}
