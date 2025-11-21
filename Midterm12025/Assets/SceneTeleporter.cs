using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleporter : MonoBehaviour
{
    [Header("场景设置")]
    public string targetSceneName;

    [Header("延迟设置（可选）")]
    public float delayAfterFadeOut = 0.5f;  // 完全黑后等待时间

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        { 
            StartCoroutine(TransitionToScene());
        }
    }

    public IEnumerator TransitionToScene()
    {
        isTransitioning = true;

        // 1. 渐变黑
        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        // 2. 可选：在黑屏时等待一下
        if (delayAfterFadeOut > 0)
        {
            yield return new WaitForSeconds(delayAfterFadeOut);
        }

        // 3. 切换场景
        SceneManager.LoadScene(targetSceneName);

        // 4. 渐变白（透明）
        yield return StartCoroutine(FadeManager.Instance.FadeIn());

        isTransitioning = false;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}