using UnityEngine;

public class ImageSwitcher : MonoBehaviour
{
    public GameObject currentImage;  // 当前显示的图片
    public GameObject nextImage;     // 下一张要显示的图片

    // 切换到下一张图（如果没有下一张，就只关闭当前图）
    public void SwitchToNext()
    {
        if (currentImage != null)
        {
            currentImage.SetActive(false);  // 关闭当前图
        }

        if (nextImage != null)
        {
            nextImage.SetActive(true);  // 打开下一张图
        }
        // 如果nextImage为null，就只关闭currentImage
    }
}