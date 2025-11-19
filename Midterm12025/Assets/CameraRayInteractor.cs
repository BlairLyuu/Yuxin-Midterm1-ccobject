// CameraRayInteractor.cs
using UnityEngine;

public class CameraRayInteractor : MonoBehaviour
{
    public enum AimMode { MousePosition, ScreenCenter }

    [Header("Aim")]
    public Camera cam;
    public AimMode aimMode = AimMode.MousePosition;
    public float maxDistance = 20f;
    public LayerMask interactLayers = ~0; // 默认全层
    public bool drawDebugRay = true;

    [Header("Input")]
    public KeyCode clickKey = KeyCode.Mouse0; // 旧输入系统
    // 新输入系统可在 Inspector 里绑一个方法：OnInteractAction()

    // 运行时状态
    private IInteractable currentHover;
    private Transform lastHitTransform;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        // 箭头从哪儿出发
        Vector3 screenPoint = aimMode == AimMode.MousePosition
            ? (Vector3)Input.mousePosition
            : new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);

        Ray ray = cam.ScreenPointToRay(screenPoint);

        if (drawDebugRay)
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.cyan);

        // 发射
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayers, QueryTriggerInteraction.Ignore))
        {
            HandleHover(hit);
            // 点击交互（旧输入系统）
            if (Input.GetKeyDown(clickKey))
            {
                currentHover?.Interact();
            }
        }
        else
        {
            ClearHover();
        }
    }

    // 新输入系统（Input System）事件回调，按需在 Inspector 绑定这个方法
    public void OnInteractAction()
    {
        currentHover?.Interact();
    }

    private void HandleHover(RaycastHit hit)
    {
        // 找 IInteractable（先从命中的 Transform 往上找）
        if (hit.transform != lastHitTransform)
        {
            // 目标切换：先清前一个
            ClearHover();

            lastHitTransform = hit.transform;
            currentHover = hit.transform.GetComponentInParent<IInteractable>();

            if (currentHover != null)
                currentHover.OnHoverEnter();
        }
        else
        {
            // 命中同一对象就啥也不干，维持 hover
            if (currentHover == null)
            {
                currentHover = hit.transform.GetComponentInParent<IInteractable>();
                if (currentHover != null) currentHover.OnHoverEnter();
            }
        }
    }

    private void ClearHover()
    {
        if (currentHover != null)
            currentHover.OnHoverExit();

        currentHover = null;
        lastHitTransform = null;
    }
}
