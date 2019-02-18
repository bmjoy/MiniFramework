namespace MiniFramework
{
    public interface IUIPanel
    {
        string Name { get; }
        void Init();
        void Open();
        void Close();
        void OnDestroy();
    }
}