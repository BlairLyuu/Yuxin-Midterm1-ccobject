using UnityEngine;

public class MouseLockController : MonoBehaviour
{
    /// <summary>
    /// 锁定鼠标
    /// </summary>
    /// 
    public MinePlayerController minePlayerControllerScript;


    public void LockMouse()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        minePlayerControllerScript.SetMouseLock(true);
        TimeResume();
        Debug.Log("Mouse Locked");
    }

    /// <summary>
    /// 解锁鼠标
    /// </summary>
    public void UnlockMouse()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        minePlayerControllerScript.SetMouseLock(false);
        TimeStop();
    }
    public void TimeStop()
    {
        Time.timeScale = 0f;
        Debug.Log("Time Stopped");
    }
    public void TimeResume()
    {
        Time.timeScale = 1f;
        Debug.Log("Time Resumed");
    }
}