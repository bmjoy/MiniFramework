using System;
using System.Collections.Generic;
namespace MiniFramework
{
    public interface IMsgReceiver { }
    public interface IMsgSender { }
    public static class MsgDispatcher
    {
        private class MsgHandler
        {
            public readonly IMsgReceiver Receiver;
            public readonly Action<object[]> Callback;
            public MsgHandler(IMsgReceiver receiver, Action<object[]> callback)
            {
                Receiver = receiver;
                Callback = callback;
            }
        }
        /// <summary>
        /// 保存同类消息组
        /// </summary>
        static readonly Dictionary<string, List<MsgHandler>> msgHandlerDict = new Dictionary<string, List<MsgHandler>>();
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="receiverSelf">接收方</param>
        /// <param name="msgName"></param>
        /// <param name="callback"></param>
        public static void RegisterMsg(this IMsgReceiver receiverSelf, string msgName, Action<object[]> callback)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                throw new Exception("消息名不能为null!");
            }
            if (callback == null)
            {
                throw new Exception("callback不能为null!");
            }
            //确保一个消息名只有一组消息列表
            if (!msgHandlerDict.ContainsKey(msgName))
            {
                msgHandlerDict[msgName] = new List<MsgHandler>();
            }
            var handlers = msgHandlerDict[msgName];
            foreach (var item in handlers)
            {
                //防止重复注册
                if (receiverSelf == item.Receiver && callback == item.Callback)
                {
                    return;
                }
            }
            handlers.Add(new MsgHandler(receiverSelf, callback));
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msgName"></param>
        /// <param name="paramList"></param>
        public static void SendMsg(this IMsgSender sender, string msgName, params object[] paramList)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                throw new Exception("消息名不能为null!");
            }
            if (!msgHandlerDict.ContainsKey(msgName))
            {
                //Debug.Log("该消息名没有被注册");
                return;
            }
            var handlers = msgHandlerDict[msgName];
            //从后向前遍历，删除item后前面item的索引不会变化
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                var handler = handlers[i];
                if (handler.Receiver != null)
                {
                    handler.Callback(paramList);
                }
                else
                {
                    handlers.Remove(handler);
                }
            }
        }
        /// <summary>
        /// 撤销注册消息
        /// </summary>
        /// <param name="receiverSelf"></param>
        /// <param name="msgName"></param>
        /// <param name="callback"></param>
        public static void UnRegisterMsg(this IMsgReceiver receiverSelf, string msgName, Action<object[]> callback)
        {
            if (msgName == null || callback == null)
            {
                return;
            }
            var handlers = msgHandlerDict[msgName];
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                var handler = handlers[i];
                if (handler.Receiver == receiverSelf && handler.Callback == callback)
                {
                    handlers.Remove(handler);
                    break;
                }
            }
        }
    }
}