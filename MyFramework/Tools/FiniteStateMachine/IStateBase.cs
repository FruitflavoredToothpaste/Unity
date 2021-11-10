namespace MyFramework.Tools
{
    /// <summary>
    /// 状态基类
    /// </summary>
    /// <typeparam name="T">状态拥有者</typeparam>
    public interface IStateBase<T>
        where T : class
    {
        void OnEnter(T owner);

        void OnUpdate(T owner);

        void OnExit(T owner);
    }
}