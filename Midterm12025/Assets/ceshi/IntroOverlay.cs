using System.Collections;
using UnityEngine;

public class IntroOverlay : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    public float fadeIn = 0.25f;
    public float hold = 1.5f;
    public float fadeOut = 0.35f;

    void Reset() => group = GetComponentInChildren<CanvasGroup>();

    public IEnumerator PlayAll()
    {
        if (group != null)
        {
            group.alpha = 0;
            group.blocksRaycasts = true;

            yield return Fade(0, 1, fadeIn);
            yield return new WaitForSeconds(hold);
            yield return Fade(1, 0, fadeOut);

            group.blocksRaycasts = false;
        }
        else
        {
            // 没有 CanvasGroup 也能用：纯等时间
            yield return new WaitForSeconds(hold);
        }
    }

    IEnumerator Fade(float a, float b, float t)
    {
        if (t <= 0) { group.alpha = b; yield break; }
        float x = 0;
        while (x < t)
        {
            x += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(a, b, x / t);
            yield return null;
        }
        group.alpha = b;
    }
}
