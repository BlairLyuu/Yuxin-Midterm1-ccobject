using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardSceneSwitcher : MonoBehaviour
{
    [Tooltip("按下指定按键时要切换到的场景名称")]
    public string targetSceneName = "";

    [Tooltip("当前场景的ID (1, 2, 或 3)")]
    public int currentSceneID = 0; // 【重要】在 Inspector 里设置为 1, 2, 或 3

    public KeyCode triggerKey = KeyCode.Space;

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

        // --- 【新增核心逻辑】 ---
        // 1. 在切换之前，标记当前场景已完成
        if (currentSceneID > 0)
        {
            GameProgressManager.MarkSceneAsCompleted(currentSceneID);
        }
        // -------------------------

        // 2. 加载目标场景 A
        SceneManager.LoadScene(targetSceneName);
    }
}