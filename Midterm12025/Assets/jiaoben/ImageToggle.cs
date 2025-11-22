using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    [Header("要显示的图片")]
    public GameObject imageObject;  // 拖入Image物体或者包含图片的GameObject

    [Header("按键设置")]
    public KeyCode toggleKey = KeyCode.E;  // 默认是E键，可以在Inspector中修改

    [Header("初始状态")]
    public bool startHidden = true;  // 开始时是否隐藏

    private void Start()
    {
        // 设置初始状态
        if (imageObject != null)
        {
            imageObject.SetActive(!startHidden);
        }
    }

    private void Update()
    {
        // 检测按键
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleImage();
        }
    }

    // 切换图片显示/隐藏
    private void ToggleImage()
    {
        if (imageObject != null)
        {
            imageObject.SetActive(!imageObject.activeSelf);

            // 可选：输出调试信息
            if (imageObject.activeSelf)
            {
                Debug.Log("图片已显示");
            }
            else
            {
                Debug.Log("图片已隐藏");
            }
        }
        else
        {
            Debug.LogWarning("没有设置图片物体！");
        }
    }
}