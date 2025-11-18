using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance; // 单例模式

    [Header("UI设置")]
    public Image fadeImage;              // 黑色遮罩图片

    [Header("淡入淡出时间")]
    public float fadeOutTime = 1f;       // 渐变黑的时间
    public float fadeInTime = 1f;        // 渐变白（透明）的时间

    private void Awake()
    {
        // 单例模式：确保只有一个FadeManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 场景切换时不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 确保开始时是透明的
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
        }
    }

    // 淡出（渐变黑）
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeOutTime);
            fadeImage.color = color;
            yield return null;
        }

        // 确保完全黑
        color.a = 1f;
        fadeImage.color = color;
    }

    // 淡入（渐变白/透明）
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeInTime);
            fadeImage.color = color;
            yield return null;
        }

        // 确保完全透明
        color.a = 0f;
        fadeImage.color = color;
    }
}