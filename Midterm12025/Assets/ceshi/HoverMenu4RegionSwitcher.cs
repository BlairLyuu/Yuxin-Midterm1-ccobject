using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class HoverMenu4RegionShowHide : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    public Image targetImage;

    public GameObject dirtyGO;
    public GameObject century20GO;
    public GameObject cosmotiniGO;
    public GameObject tequilaAppletiniGO;

    public int CurrentHoverIndex { get; private set; } = -1;
    public int LastHoverIndex { get; private set; } = -1;   // 记录最后一次 hover 的酒
    public event Action<int> OnHoverChanged;

    bool _locked = false;

    void Reset() => targetImage = GetComponent<Image>();

    void Start() => HideAll();

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_locked) return;
        if (!targetImage) return;

        RectTransform rt = targetImage.rectTransform;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt, eventData.position, eventData.pressEventCamera, out var local))
            return;

        Rect r = rt.rect;
        float v = Mathf.Clamp01(Mathf.InverseLerp(r.yMin, r.yMax, local.y));

        int index =
            (v > 0.652f) ? 0 :
            (v > 0.465f) ? 1 :
            (v > 0.262f) ? 2 :
                           3;

        if (index == CurrentHoverIndex) return;

        CurrentHoverIndex = index;
        LastHoverIndex = index;
        ShowOnly(index);
        OnHoverChanged?.Invoke(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_locked) return;          // 锁住后离开 menu 不隐藏
        CurrentHoverIndex = -1;       // 但 LastHoverIndex 保留
        // 不 HideAll() —— 让预览保持显示，方便你去点 Confirm
    }

    public void LockSelection() => _locked = true;

    public void UnlockSelection()
    {
        _locked = false;
        CurrentHoverIndex = -1;
        LastHoverIndex = -1;
        HideAll();
    }

    void ShowOnly(int index)
    {
        if (dirtyGO) dirtyGO.SetActive(index == 0);
        if (century20GO) century20GO.SetActive(index == 1);
        if (cosmotiniGO) cosmotiniGO.SetActive(index == 2);
        if (tequilaAppletiniGO) tequilaAppletiniGO.SetActive(index == 3);
    }

    void HideAll()
    {
        if (dirtyGO) dirtyGO.SetActive(false);
        if (century20GO) century20GO.SetActive(false);
        if (cosmotiniGO) cosmotiniGO.SetActive(false);
        if (tequilaAppletiniGO) tequilaAppletiniGO.SetActive(false);
    }
}
