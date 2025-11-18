using UnityEngine;
using System.Collections;

public class SceneStarter : MonoBehaviour
{
    private void Start()
    {
        // 场景加载后自动执行淡入效果
        if (FadeManager.Instance != null)
        {
            StartCoroutine(FadeManager.Instance.FadeIn());
        }
    }
}