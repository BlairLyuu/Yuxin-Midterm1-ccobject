using UnityEngine;
using DG.Tweening;

public class MoveOnPath : MonoBehaviour
{
    public Transform obj;
    public Transform A;
    public Transform B;
    public Transform C; // 中间控制点（可选，让路径弯起来）

    void Start()
    {
        Vector3[] path = new Vector3[]
        {
            A.position,
            C.position, // 没有就删掉这一行
            B.position
        };

        obj.DOPath(
                path,
                45f,                     // 持续时间
                PathType.CatmullRom,    // 平滑曲线
                PathMode.Full3D         // 3D路径
            )
            .SetEase(Ease.InOutSine)
            .SetLookAt(0.01f);         // 可选：朝运动方向看
    }
}
