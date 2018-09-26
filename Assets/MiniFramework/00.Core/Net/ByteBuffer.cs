using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace MiniFramework
{
    public static class ByteBuffer
    {
        /// <summary>
        /// 封包
        /// </summary>
        /// <param name="command"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] BuildDataPackage(int command, byte[] data)
        {
            //命令码
            byte[] commandBytes = BitConverter.GetBytes(command);
            int bodyLength = commandBytes.Length + data.Length;
            //消息头
            byte[] headBytes = BitConverter.GetBytes(bodyLength);
            //数据包
            byte[] totalBytes = new byte[headBytes.Length + bodyLength];
            headBytes.CopyTo(totalBytes, 0);
            commandBytes.CopyTo(totalBytes, headBytes.Length);
            data.CopyTo(totalBytes, headBytes.Length + commandBytes.Length);
            return totalBytes;
        }
    }
}