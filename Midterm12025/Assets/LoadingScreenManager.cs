using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager instance { get; private set; }
    public CanvasGroup loadingScreen;
    private Coroutine screenAlphaCoroutine;
    private const float FadeDuration = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowLoadingScreen()
    {
        if (loadingScreen == null) return;
        // 停掉旧协程，避免打架
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        // 先确保可拦截输入，再渐入
        ScreenInteractivity(true);
        screenAlphaCoroutine = StartCoroutine(FadeCanvasAlpha(loadingScreen, targetAlpha: 1f, duration: FadeDuration));
    }

    public void EndLoadingScreen()
    {
        if (loadingScreen == null) return;
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        screenAlphaCoroutine = StartCoroutine(FadeOutThenDisableInput());
    }

    public void ShowLoadingScreen_fast()
    {
        if (loadingScreen == null) return;
        // 停掉旧协程，避免打架
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        // 先确保可拦截输入，再渐入
        ScreenInteractivity(true);
        screenAlphaCoroutine = StartCoroutine(FadeCanvasAlpha(loadingScreen, targetAlpha: 1f, duration: 0.25f));
    }

    public void EndLoadingScreen_fast()
    {
        if (loadingScreen == null) return;
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        screenAlphaCoroutine = StartCoroutine(FadeOutThenDisableInput_fast());
    }

    public void ForceShowLoadingScreen()
    {
        if (loadingScreen == null) return;
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        loadingScreen.alpha = 1f;
        ScreenInteractivity(true);
        //loadingScreen.gameObject.SetActive(true);
    }

    public void ForceEndLoadingScreen()
    {
        if (loadingScreen == null) return;
        if (screenAlphaCoroutine != null) StopCoroutine(screenAlphaCoroutine);

        loadingScreen.alpha = 0f;
        ScreenInteractivity(false);
       // loadingScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutThenDisableInput()
    {
        yield return FadeCanvasAlpha(loadingScreen, targetAlpha: 0f, duration: FadeDuration);
        ScreenInteractivity(false);
    }
    private IEnumerator FadeOutThenDisableInput_fast()
    {
        yield return FadeCanvasAlpha(loadingScreen, targetAlpha: 0f, duration: 0.25f);
        ScreenInteractivity(false);
    }

    private IEnumerator FadeCanvasAlpha(CanvasGroup group, float targetAlpha, float duration)
    {
        float startAlpha = group.alpha;
        float elapsed = 0f;
        // 防抖：极短时长或目标已到，直接赋值
        if (Mathf.Approximately(startAlpha, targetAlpha) || duration <= 0f)
        {
            group.alpha = targetAlpha;
            yield break;
        }

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // 线性插值；需要更丝滑可以换成 SmoothStep
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        group.alpha = targetAlpha;
    }

    void ScreenInteractivity(bool canInteract)
    {
        loadingScreen.blocksRaycasts = canInteract; 
        loadingScreen.interactable = canInteract;  
    }
}
