using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardSceneSwitcher : MonoBehaviour
{
    [Tooltip("按下指定按键时要切换到的场景名称")]
    public string targetSceneName = "";

    [Tooltip("当前场景的ID (1, 2, 或 3)")]
    public int currentSceneID = 0; // Inspector 设置 1/2/3

    public KeyCode triggerKey = KeyCode.Space;

    [Header("计数：返回大场景时 +1")]
    [Tooltip("你的大场景名字（Build Settings 里显示的名字）")]
    public string mainSceneName = "midterm"; // 这里改成你的大场景名

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning("目标场景名称为空！");
            return;
        }

        // 1) 切换前：标记当前小场景完成（你原本的逻辑）
        if (currentSceneID > 0)
        {
            GameProgressManager.MarkSceneAsCompleted(currentSceneID);
        }

        // 2) 新增：如果这次是“回到大场景”，写一个返回标记，让大场景去 +1
        if (targetSceneName == mainSceneName)
        {
            PlayerPrefs.SetInt("ReturnedFromMini", 1);
            PlayerPrefs.SetInt("LastMiniSceneID", currentSceneID);
            PlayerPrefs.SetString("LastMiniSceneName", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
        }

        // 3) 加载目标场景
        SceneManager.LoadScene(targetSceneName);
    }
}
