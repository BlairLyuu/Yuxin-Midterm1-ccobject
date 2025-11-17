using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全局图片显示管理器 (单例模式)
/// 管理所有的图片显示，点击任何地方关闭图片
/// </summary>
public class ImageDisplayManager : MonoBehaviour
{
    public static ImageDisplayManager Instance { get; private set; }

    [Header("UI组件")]
    public GameObject imagePanel;        // 图片面板
    public Image displayImage;           // 显示图片的Image组件
    public Button closeButton;           // 关闭按钮（可选）

    [Header("设置")]
    public bool clickAnywhereToClose = true;  // 点击任何地方关闭

    private bool isImageShowing = false;

    void Awake()
    {
        // 单例模式设置
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 初始隐藏
        if (imagePanel)
            imagePanel.SetActive(false);

        // 设置关闭按钮（如果有）
        if (closeButton)
        {
            closeButton.onClick.AddListener(CloseImage);
        }
    }

    void Update()
    {
        // 点击屏幕关闭图片
        if (isImageShowing && clickAnywhereToClose && Input.GetMouseButtonDown(0))
        {
            // 检查是否点击在UI上（避免点击按钮时关闭）
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                CloseImage();
            }
        }
    }

    /// <summary>
    /// 显示图片
    /// </summary>
    public void ShowImage(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("尝试显示空图片！");
            return;
        }

        displayImage.sprite = sprite;
        imagePanel.SetActive(true);
        isImageShowing = true;
    }

    /// <summary>
    /// 关闭图片
    /// </summary>
    public void CloseImage()
    {
        imagePanel.SetActive(false);
        isImageShowing = false;
    }
}