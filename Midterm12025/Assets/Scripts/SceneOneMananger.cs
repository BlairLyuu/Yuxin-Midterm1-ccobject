using System.Collections.Generic;
using UnityEngine;

public class SceneOneMananger : MonoBehaviour
{
    public int SceneOneIndex;

    public GameObject Player;
    public Transform TargetTrans;
    public AudioSource AudioPlayer;
    public List<AudioClip> audioClips;
    private void Start()
    {
        // 1) 读取保存的次数
        SceneOneIndex = PlayerPrefs.GetInt("SceneOneIndex", 0);

        // 2) 检测是否从小场景返回（只在返回时 +1）
        int returned = PlayerPrefs.GetInt("ReturnedFromMini", 0);
        if (returned == 1)
        {
            SceneOneIndex++;
            PlayerPrefs.SetInt("SceneOneIndex", SceneOneIndex);

            // 清标记，避免重复加
            PlayerPrefs.SetInt("ReturnedFromMini", 0);
            PlayerPrefs.Save();
        }

        AudioPlayer.clip = audioClips[SceneOneIndex];
        AudioPlayer.Play();


        Debug.Log("SceneOneIndex: " + SceneOneIndex);

        // 3) 回来后传送玩家（可选）
        if (SceneOneIndex > 0 && Player != null && TargetTrans != null)
        {
            Player.transform.position = TargetTrans.position;
        }

        // 4) 刷新 UI（可选）
        var ui = FindFirstObjectByType<SceneVisitUI>();
        if (ui != null) ui.Refresh(SceneOneIndex);
    }

    // 如果你还想保留手动加次数的方法（比如按钮触发）
    public void OnChangerScene()
    {
        SceneOneIndex++;
        PlayerPrefs.SetInt("SceneOneIndex", SceneOneIndex);
        PlayerPrefs.Save();

        Debug.Log("SceneOneIndex: " + SceneOneIndex);

        var ui = FindFirstObjectByType<SceneVisitUI>();
        if (ui != null) ui.Refresh(SceneOneIndex);
    }
}
