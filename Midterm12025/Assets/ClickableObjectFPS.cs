using UnityEngine;

/// <summary>
/// 第一人称游戏专用的可点击物体脚本
/// 使用 Raycast 射线检测，而不是 OnMouseDown
/// </summary>
public class ClickableObjectFPS : MonoBehaviour
{
    [Header("要展示的图片")]
    public Sprite displayImage;

    [Header("高亮效果设置")]
    public bool useGlowEffect = true;
    public Color glowColor = Color.white;
    public float glowIntensity = 2f;

    [Header("UI提示（可选）")]
    public bool showHoverText = true;
    public string hoverText = "按 E 查看";

    private Material originalMaterial;
    private Material glowMaterial;
    private Renderer objectRenderer;
    private bool isLookingAt = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogWarning("物体 " + gameObject.name + " 没有 Renderer 组件，发光效果不可用");
        }

        // 如果使用发光效果，创建发光材质
        if (useGlowEffect && objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
            glowMaterial = new Material(originalMaterial);
            glowMaterial.EnableKeyword("_EMISSION");
            Debug.Log("✓ " + gameObject.name + " 发光材质已创建");
        }

        Debug.Log("✓ ClickableObjectFPS 初始化完成: " + gameObject.name);
        if (displayImage != null)
        {
            Debug.Log("  - 分配的图片: " + displayImage.name);
        }
        else
        {
            Debug.LogWarning("  - ⚠ 没有分配图片！");
        }
    }

    // 当玩家看着这个物体时被调用
    public void OnLookAt()
    {
        Debug.Log("→→→ 玩家看向: " + gameObject.name);

        if (!isLookingAt)
        {
            isLookingAt = true;

            // 应用发光效果
            if (useGlowEffect && objectRenderer != null)
            {
                objectRenderer.material = glowMaterial;
                glowMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
                Debug.Log("✓ 发光效果已应用");
            }
        }
    }

    // 当玩家不再看这个物体时被调用
    public void OnLookAway()
    {
        Debug.Log("←←← 玩家移开视线: " + gameObject.name);

        if (isLookingAt)
        {
            isLookingAt = false;

            // 移除发光效果
            if (useGlowEffect && objectRenderer != null)
            {
                objectRenderer.material = originalMaterial;
                Debug.Log("✓ 发光效果已移除");
            }
        }
    }

    // 当玩家点击这个物体时被调用
    public void OnClicked()
    {
        Debug.Log("✓✓✓ 点击了物体: " + gameObject.name);

        // 显示图片
        if (displayImage != null)
        {
            Debug.Log("准备显示图片: " + displayImage.name);

            if (ImageDisplayManager.Instance != null)
            {
                Debug.Log("✓ ImageDisplayManager 存在，调用 ShowImage");
                ImageDisplayManager.Instance.ShowImage(displayImage);
                Debug.Log("✓ ShowImage 调用完成");
            }
            else
            {
                Debug.LogError("❌ 找不到 ImageDisplayManager！");
            }
        }
        else
        {
            Debug.LogWarning("❌ 没有分配要显示的图片！");
        }
    }

    // 获取提示文本
    public string GetHoverText()
    {
        return hoverText;
    }

    // 是否正在被看着
    public bool IsLookingAt()
    {
        return isLookingAt;
    }
}