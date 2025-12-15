using UnityEngine;
using TMPro;

public class OnConfirmClick : MonoBehaviour
{
    [Header("Hover script on Menu Image")]
    public HoverMenu4RegionShowHide hover;

    [Header("Only this block changes")]
    public GameObject infoPanel;   // 你的那块“会出现的Canvas/Panel”
    public TMP_Text infoText;      // Panel 里的 TMP 文本

    [Header("Text for each drink (0~3)")]
    [TextArea] public string[] drinkTexts = new string[4];

    // 绑定到 Button OnClick()
    public void Confirm()
    {
        if (hover == null) { Debug.LogError("Missing hover reference"); return; }

        int i = hover.LastHoverIndex;   // 0~3
        if (i < 0 || i >= drinkTexts.Length) return;

        hover.LockSelection(); // 锁住：离开menu去点按钮，预览也不消失

        if (infoPanel) infoPanel.SetActive(true);
        if (infoText) infoText.text = drinkTexts[i];
    }

    // 可选：做一个“重新选择/关闭面板”
    public void ResetSelection()
    {
        if (hover != null) hover.UnlockSelection();
        if (infoPanel) infoPanel.SetActive(false);
        if (infoText) infoText.text = "";
    }
}
