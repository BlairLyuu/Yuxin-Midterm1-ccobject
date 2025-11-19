using UnityEngine;

public class InteractableSample : MonoBehaviour
{   
    [Header("Ray Settings")]
    [Tooltip("哪台相机来发射屏幕中心的射线。为空则用 Camera.main")]
    public Camera cam;
    public bool aimAtCenter = true;       // false 则用鼠标
    public float maxDistance = 50f;
    public LayerMask layers = ~0;
    public bool includeTriggers = false;
    public bool drawRay = true;

    [Header("Input")]
    public KeyCode clickKey = KeyCode.Mouse0;

    private bool _hovering;

    EnumSample sample;


    void Awake()
    {
        if (!cam) cam = Camera.main;
        if (!cam) Debug.LogError("[SceneButtons] 没相机引用！请在 Inspector 赋值或设置 MainCamera。");
    }

    void OnEnable()
    {
        _hovering = false;
    }

    void Update()
    {
        // 每个挂有 SceneButtons 的物体都自己做一次“我是不是被指到了”的判定
        if (!cam) return;

        Vector3 sp = aimAtCenter
            ? new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)
            : (Vector3)Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(sp);
        if (drawRay) Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.yellow);

        var qti = includeTriggers ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore;
        if (Physics.Raycast(ray, out var hit, maxDistance, layers, qti))
        {
            bool hitThis = hit.transform == transform || hit.transform.IsChildOf(transform);
            if (hitThis)
            {
                if (!_hovering) OnHoverEnter();
                if (Input.GetKeyDown(clickKey) && _hovering)
                {
                    OnHoverClick();
                }
                return;
            }
        }

        if (_hovering) OnHoverExit();
    }

    public virtual void OnHoverClick()
    {
        Debug.Log("玩家点击");

    }
   
    private void OnHoverEnter()
    {
        _hovering = true;
        Debug.Log("玩家瞄准");
    }

    private void OnHoverExit()
    {
        _hovering = false;
        Debug.Log("玩家退出瞄准");
    }

}
