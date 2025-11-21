using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher2 : MonoBehaviour
{
    // 在Inspector中拖入按钮
    public Button switchButton;

    // 要切换到的场景名称
    public string targetSceneName;

    void Start()
    {
        // 给按钮添加点击事件
        if (switchButton != null)
        {
            switchButton.onClick.AddListener(SwitchScene);
        }
    }

    // 切换场景的方法
    void SwitchScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("目标场景名称为空！");
        }
    }

    // 也可以用场景索引来切换
    public void SwitchSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}