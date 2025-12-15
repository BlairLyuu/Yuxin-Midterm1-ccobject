using UnityEngine;
using TMPro;

public class ButtonShowCanvasContent : MonoBehaviour
{
    [Header("Picture canvases (already placed in UI)")]
    public GameObject[] pictureCanvases; // 0~n：你已放好的图片Canvas/Panel

    [Header("Text to change (only this block)")]
    public TMP_Text infoText;            // 你要替换的那段文字

    [TextArea]
    public string[] texts;               // 每个按钮对应的文字（长度至少 = pictureCanvases.Length）

    [Header("Optional")]
    public bool hideAllOnStart = true;

    void Start()
    {
        if (hideAllOnStart) HideAllPictures();
    }

    public void Show(int index)
    {
        if (pictureCanvases == null || pictureCanvases.Length == 0) return;
        if (index < 0 || index >= pictureCanvases.Length) return;

        // 1) 图片：只显示选中的那一个
        for (int i = 0; i < pictureCanvases.Length; i++)
        {
            if (pictureCanvases[i] != null)
                pictureCanvases[i].SetActive(i == index);
        }

        // 2) 文字：替换对应文本
        if (infoText != null && texts != null && index < texts.Length)
            infoText.text = texts[index];
    }

    public void HideAllPictures()
    {
        if (pictureCanvases == null) return;
        foreach (var go in pictureCanvases)
            if (go != null) go.SetActive(false);
    }
}
