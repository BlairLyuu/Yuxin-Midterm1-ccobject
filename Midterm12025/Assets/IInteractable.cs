// IInteractable.cs
public interface IInteractable
{
    /// 当相机射线开始指向该对象
    void OnHoverEnter();

    /// 当相机射线离开该对象
    void OnHoverExit();

    /// 当玩家点击（或触发交互键）时
    void Interact();
}
public enum EnumSample
{

    Sample1,
    Sample2,
    Sample123,
    Sample2412,
    Sample4211,
    Sample1212,
    Sample111,
    Sample2342,
}

