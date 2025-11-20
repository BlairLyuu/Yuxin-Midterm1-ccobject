using UnityEngine;

public class MouseLockController : MonoBehaviour
{
    /// <summary>
    /// 锁定鼠标
    /// </summary>
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// 解锁鼠标
    /// </summary>
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}