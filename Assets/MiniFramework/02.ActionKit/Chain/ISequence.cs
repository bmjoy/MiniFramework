using System;
namespace MiniFramework
{
    /// <summary>
    /// 队列接口
    /// </summary>
    public interface ISequence
    {
        void Append(float seconds);
        void Append(Action action);
        void Append(Func<bool> condition);
        void Execute();
    }
}