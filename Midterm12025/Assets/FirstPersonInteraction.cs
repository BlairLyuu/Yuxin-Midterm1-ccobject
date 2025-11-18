using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 第一人称交互管理器
/// 从摄像机中心发射射线检测可点击物体
/// </summary>
public class FirstPersonInteraction : MonoBehaviour
{
    [Header("交互设置")]
    public KeyCode interactKey = KeyCode.E;          // 交互按键（默认E）
    public float interactionDistance = 3f;           // 交互距离
    public LayerMask interactableLayer;              // 可交互物体的层级（可选）

    [Header("UI提示（可选）")]
    public Text hoverTextUI;                         // 显示提示文字的UI Text
    public GameObject crosshair;                     // 准星（可选）

    [Header("调试")]
    public bool showDebugRay = true;                 // 是否显示调试射线

    private Camera playerCamera;
    private ClickableObjectFPS currentLookingObject;

    void Start()
    {
        // 获取摄像机
        playerCamera = GetComponent<Camera>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (playerCamera == null)
        {
            Debug.LogError("❌ 找不到摄像机！FirstPersonInteraction脚本需要附加在有Camera组件的物体上！");
        }
        else
        {
            Debug.Log("✓ 摄像机找到了: " + playerCamera.name);
        }

        // 初始化UI
        if (hoverTextUI)
            hoverTextUI.text = "";

        Debug.Log("✓ FirstPersonInteraction 初始化完成");
        Debug.Log("交互按键: " + interactKey);
        Debug.Log("交互距离: " + interactionDistance);
    }

    void Update()
    {
        if (playerCamera == null)
        {
            Debug.LogError("摄像机为空！");
            return;
        }

        // 从摄像机中心发射射线
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(ray, out hit, 100f, interactableLayer);

        // 调试：显示射线
        if (showDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.yellow);
        }

        if (hitSomething)
        {
            // 如果没有设置层级，检测所有物体
            //hitSomething = Physics.Raycast(ray, out hit, interactionDistance);
            //call the functions on your interactable object here
            GameObject target = hit.collider.gameObject;
            ClickableObjectFPS objectScript = target.GetComponent<ClickableObjectFPS>();

            if (Input.GetKeyUp(interactKey))
            {
                //code that should run when the player clicks on an object goes here
            }
        }

        if (hitSomething)
        {
            Debug.Log("射线击中了: " + hit.collider.gameObject.name);

            // 检查击中的物体是否有 ClickableObjectFPS 组件
            ClickableObjectFPS clickable = hit.collider.GetComponent<ClickableObjectFPS>();

            if (clickable != null)
            {
                Debug.Log("✓ 这是一个可点击物体！");

                // 如果是新的物体
                if (currentLookingObject != clickable)
                {
                    Debug.Log("→ 看向新物体");

                    // 清除之前的物体状态
                    if (currentLookingObject != null)
                    {
                        currentLookingObject.OnLookAway();
                    }

                    // 设置新的物体
                    currentLookingObject = clickable;
                    currentLookingObject.OnLookAt();

                    // 显示提示文字
                    if (hoverTextUI)
                    {
                        hoverTextUI.text = clickable.GetHoverText();
                    }
                }

                // 检测交互按键
                if (Input.GetKeyDown(interactKey))
                {
                    Debug.Log("✓✓✓ 按下了交互键！");
                    clickable.OnClicked();
                }
            }
            else
            {
                Debug.Log("击中的物体没有 ClickableObjectFPS 组件");
                // 击中了物体但不是可点击的
                ClearCurrentObject();
            }
        }
        else
        {
            // 没有击中任何物体
            ClearCurrentObject();
        }
    }

    void ClearCurrentObject()
    {
        if (currentLookingObject != null)
        {
            currentLookingObject.OnLookAway();
            currentLookingObject = null;

            if (hoverTextUI)
            {
                hoverTextUI.text = "";
            }
        }
    }
}