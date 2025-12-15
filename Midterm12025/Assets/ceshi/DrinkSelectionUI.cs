using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrinkSelectionSinglePanel : MonoBehaviour
{
    [Header("Hover script (挂在 Menu 那张图上)")]
    public HoverMenu4RegionShowHide hover;

    [Header("Confirm Button (可选：拖了就自动绑定点击)")]
    public Button confirmButton;

    [Header("Only this block changes")]
    public GameObject infoPanel;   // 例如：Paneljiu
    public TMP_Text infoText;      // 例如：Paneljiu 里面的 Text (TMP)

    [Header("Text for each drink (index 0~3)")]
    [TextArea] public string[] drinkTexts = new string[4];

    [Header("Options")]
    public bool lockOnConfirm = true;     // 点 confirm 后锁定选择
    public bool hidePanelOnStart = true;  // 开场隐藏面板

    void Awake()
    {
        if (confirmButton != null)
            confirmButton.onClick.AddListener(Confirm);
    }

    void Start()
    {
        if (hidePanelOnStart && infoPanel != null)
            infoPanel.SetActive(false);

        RefreshConfirmInteractable();
    }

    void OnEnable()
    {
        if (hover != null)
            hover.OnHoverChanged += HandleHoverChanged;

        RefreshConfirmInteractable();
    }

    void OnDisable()
    {
        if (hover != null)
            hover.OnHoverChanged -= HandleHoverChanged;
    }

    void HandleHoverChanged(int hoverIndex)
    {
        RefreshConfirmInteractable();
    }

    void RefreshConfirmInteractable()
    {
        if (confirmButton == null) return;
        if (hover == null) { confirmButton.interactable = false; return; }

        // 只要 hover 过一次（LastHoverIndex >=0），就允许点 confirm
        confirmButton.interactable = (hover.LastHoverIndex >= 0);
    }

    // ? 你可以把这个函数绑到 Button 的 OnClick()，也可以靠上面的自动绑定
    public void Confirm()
    {
        if (hover == null)
        {
            Debug.LogError("DrinkSelectionSinglePanel: hover reference missing.");
            return;
        }

        int i = hover.LastHoverIndex; // 鼠标离开 menu 也能拿到
        if (i < 0 || i >= drinkTexts.Length)
        {
            Debug.LogWarning("DrinkSelectionSinglePanel: no valid selection yet.");
            return;
        }

        if (lockOnConfirm)
            hover.LockSelection();

        if (infoPanel != null) infoPanel.SetActive(true);
        if (infoText != null) infoText.text = drinkTexts[i];
    }

    // 可选：做一个“重新选择 / 关闭面板”按钮时用
    public void ResetSelection()
    {
        if (hover != null) hover.UnlockSelection();
        if (infoPanel != null) infoPanel.SetActive(false);
        if (infoText != null) infoText.text = "";
        RefreshConfirmInteractable();
    }
}
