using System;
namespace MiniFramework
{
    public sealed class MsgSender : MsgDispatcher
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgName"></param>
        /// <param name="paramList"></param>
        public static void SendMsg(string msgName, params object[] paramList)
        {
            if (!msgHandlerDict.ContainsKey(msgName))
            {
                throw new Exception("该消息名没有被注册");
            }
            var handlers = msgHandlerDict[msgName];
            //从后向前遍历，删除item后前面item的索引不会变化
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                MsgHandler handler = handlers[i];
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
    }
}

