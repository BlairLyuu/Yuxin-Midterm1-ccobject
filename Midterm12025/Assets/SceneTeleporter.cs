using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleporter : MonoBehaviour
{
    [Header("场景设置")]
    public string targetSceneName;

    [Header("Intro 图片（prefab）")]
    public GameObject introPrefab;              // 拖你的 intro prefab
    public Transform introParent;               // 可空：不填也行
    public float introFallbackDuration = 2f;    // prefab 没有脚本时，纯等待这么久

    [Header("延迟设置（可选）")]
    public float delayAfterFadeOut = 0.5f;      // 完全黑后等待时间

    private bool isTransitioning = false;

    public SceneOneMananger oneMananger;
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
        oneMananger.OnChangerScene();
        // A) 先放 Intro（在 Scene1 里播放完）
        GameObject introObj = null;
        if (introPrefab != null)
        {
            introObj = Instantiate(introPrefab, introParent);

            // 如果你的 intro prefab 上有 IntroOverlay 脚本，就等它播完
            var intro = introObj.GetComponent<IntroOverlay>();
            if (intro != null)
            {
                yield return intro.PlayAll();
            }
            else
            {
                // 没脚本就按一个固定时长等
                yield return new WaitForSeconds(introFallbackDuration);
            }

            Destroy(introObj);
        }

        // B) 再渐黑（你原来的流程）
        if (FadeManager.Instance != null)

           StartCoroutine(FadeManager.Instance.FadeOut());

        yield return new WaitForSeconds(2);

        if (delayAfterFadeOut > 0)
            yield return new WaitForSeconds(delayAfterFadeOut);


        // C) 切换场景
        SceneManager.LoadScene(targetSceneName);

       
    }

    public void NextScene()
    {
        // 建议也走同一个过场逻辑
        if (!isTransitioning)
            StartCoroutine(TransitionToScene());
    }
}
