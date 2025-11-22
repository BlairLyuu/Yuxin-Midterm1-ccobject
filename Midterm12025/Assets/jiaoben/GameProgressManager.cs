using UnityEngine;

// 这是一个静态类，用于在场景切换时保留数据
public static class GameProgressManager
{
    // 静态变量，用于记录场景1、2、3是否已完成（已返回主场景A）
    public static bool Scene1Completed = false;
    public static bool Scene2Completed = false;
    public static bool Scene3Completed = false;

    // 可以在任何地方调用这个方法来设置某个场景已完成
    public static void MarkSceneAsCompleted(int sceneID)
    {
        if (sceneID == 1)
        {
            Scene1Completed = true;
            Debug.Log("进度更新: 场景 1 已完成。");
        }
        else if (sceneID == 2)
        {
            Scene2Completed = true;
            Debug.Log("进度更新: 场景 2 已完成。");
        }
        else if (sceneID == 3)
        {
            Scene3Completed = true;
            Debug.Log("进度更新: 场景 3 已完成。");
        }
        else
        {
            Debug.LogWarning("尝试标记一个无效的场景ID。");
        }
    }
}