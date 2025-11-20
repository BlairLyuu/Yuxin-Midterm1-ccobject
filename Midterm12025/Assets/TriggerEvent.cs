using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [Header("触发设置")]
    [Tooltip("触发器的标签，留空则任何物体都会触发")]
    public string targetTag = "Player";

    [Tooltip("是否只触发一次")]
    public bool triggerOnce = false;

    [Header("事件")]
    [Space(10)]
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerStayEvent;
    public UnityEvent onTriggerExitEvent;

    // 带参数的事件（传递碰撞的GameObject）
    [System.Serializable]
    public class ColliderEvent : UnityEvent<GameObject> { }

    [Header("高级事件（带参数）")]
    [Space(10)]
    public ColliderEvent onTriggerEnterWithObject;
    public ColliderEvent onTriggerExitWithObject;

    private bool hasTriggered = false;

    void Start()
    {
        // 确保有Collider并且是Trigger
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("TriggerEvent: 没有找到Collider组件！", this);
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning("TriggerEvent: Collider不是Trigger模式！", this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 如果设置了只触发一次，并且已经触发过了，就返回
        if (triggerOnce && hasTriggered)
            return;

        // 检查tag（如果设置了的话）
        if (!string.IsNullOrEmpty(targetTag) && !other.CompareTag(targetTag))
            return;

        // 触发事件
        onTriggerEnterEvent?.Invoke();
        onTriggerEnterWithObject?.Invoke(other.gameObject);

        hasTriggered = true;

        // 可选：输出调试信息
        Debug.Log($"[TriggerEvent] {other.name} 进入了触发器 {gameObject.name}");
    }

    void OnTriggerStay(Collider other)
    {
        // 检查tag
        if (!string.IsNullOrEmpty(targetTag) && !other.CompareTag(targetTag))
            return;

        onTriggerStayEvent?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        // 检查tag
        if (!string.IsNullOrEmpty(targetTag) && !other.CompareTag(targetTag))
            return;

        onTriggerExitEvent?.Invoke();
        onTriggerExitWithObject?.Invoke(other.gameObject);

        Debug.Log($"[TriggerEvent] {other.name} 离开了触发器 {gameObject.name}");
    }

    // 重置触发状态的公共方法
    public void ResetTrigger()
    {
        hasTriggered = false;
    }

    // 手动触发事件的方法（用于测试或其他用途）
    public void ManualTrigger()
    {
        onTriggerEnterEvent?.Invoke();
    }
}