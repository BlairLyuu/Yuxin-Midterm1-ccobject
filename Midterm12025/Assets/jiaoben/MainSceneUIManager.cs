using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    // 【重要】在 Inspector 中拖入代表场景1, 2, 3完成状态的 UI 元素 (比如 Image)
    public GameObject scene1CompleteIndicator;
    public GameObject scene2CompleteIndicator;
    public GameObject scene3CompleteIndicator;

    // 场景加载后立即执行
    void Start()
    {
        // 初始化时先隐藏所有指示器
        scene1CompleteIndicator.SetActive(false);
        scene2CompleteIndicator.SetActive(false);
        scene3CompleteIndicator.SetActive(false);

        // 检查进度管理器中的数据并更新 UI
        CheckAndDisplayProgress();
    }

    private void CheckAndDisplayProgress()
    {
        // 检查场景 1 是否已完成
        if (GameProgressManager.Scene1Completed && scene1CompleteIndicator != null)
        {
            scene1CompleteIndicator.SetActive(true);
        }

        // 检查场景 2 是否已完成
        if (GameProgressManager.Scene2Completed && scene2CompleteIndicator != null)
        {
            scene2CompleteIndicator.SetActive(true);
        }

        // 检查场景 3 是否已完成
        if (GameProgressManager.Scene3Completed && scene3CompleteIndicator != null)
        {
            scene3CompleteIndicator.SetActive(true);
        }

        // 提示：你也可以在这里修改 Text 组件的内容，比如从 "未完成" 改为 "已通过"
        // if (GameProgressManager.Scene1Completed)
        // {
        //     scene1CompleteIndicator.GetComponent<Text>().text = "场景一：已通过";
        // }
    }
}