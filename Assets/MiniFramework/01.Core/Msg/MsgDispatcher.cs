using System;
using System.Collections.Generic;
namespace MiniFramework
{
    public class MsgDispatcher
    {
        protected class MsgHandler
        {
            public readonly object Receiver;
            public readonly Action<object[]> Callback;
            public MsgHandler(object receiver, Action<object[]> callback)
            {
                Receiver = receiver;
                Callback = callback;
            }
        }
        /// <summary>
        /// 保存同类消息组
        /// </summary>
        protected static readonly Dictionary<string, List<MsgHandler>> msgHandlerDict = new Dictionary<string, List<MsgHandler>>();          
        /// <summary>
        /// 撤销注册消息
        /// </summary>
        /// <param name="receiverSelf"></param>
        /// <param name="msgName"></param>
        /// <param name="callback"></param>
        public static void UnRegisterMsg(object receiver, string msgName, Action<object[]> callback)
        {
            if (callback == null)
            {
                return;
            }
            var handlers = msgHandlerDict[msgName];
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                var handler = handlers[i];
                if (handler.Receiver == receiver && handler.Callback == callback)
                {
                    handlers.Remove(handler);
                    break;
                }
            }
        }
    }
}