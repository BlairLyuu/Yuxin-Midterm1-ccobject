using UnityEngine;

public class NextAnimation : MonoBehaviour
{
    private Animator anim;

    [Header("UI 设置")]
    // 1. 这里放你当前正在显示的 Canvas (按C后要关闭的)
    public GameObject currentCanvas;

    // 2. 这里放你想要打开的下一个 Canvas (按C后要出现的)
    public GameObject nextCanvas;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayNext();
            SwitchCanvas(); // 执行切换 UI 的操作
        }
    }

    void PlayNext()
    {
        if (anim != null)
        {
            anim.SetTrigger("Next");
        }
    }

    // 专门用来处理“关一个、开一个”的逻辑
    void SwitchCanvas()
    {
        // 1. 关闭当前显示的
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }

        // 2. 打开下一个
        if (nextCanvas != null)
        {
            nextCanvas.SetActive(true);
        }
    }
}