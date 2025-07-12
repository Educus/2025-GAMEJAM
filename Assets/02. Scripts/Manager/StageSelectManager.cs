using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    public void NextScenes(int value)
    {
        ScenesManager.Instance.LoadScene(value);
    }
}
