using System;
namespace MiniFramework
{
    public abstract class SocketBase
    {
        public Action ConnectFailed;
        public Action ConnectSuccess;
        public abstract void Launch();
        public abstract void Send(byte[] data);
        public abstract void Close();
    }
}

