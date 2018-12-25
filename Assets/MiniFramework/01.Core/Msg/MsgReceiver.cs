using System;
using System.Collections.Generic;

namespace MiniFramework {
    public sealed class MsgReceiver : MsgDispatcher{
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="receiverSelf">接收方</param>
        /// <param name="msgName"></param>
        /// <param name="callback"></param>
        public static void RegisterMsg(object receiverSelf, string msgName, Action<object[]> callback)
        {
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
    }
}

