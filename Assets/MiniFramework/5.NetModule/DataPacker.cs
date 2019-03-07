using System;
using System.Runtime.InteropServices;
using UnityEngine;
namespace MiniFramework
{
    public class DataPacker
    {
        public byte[] OtherBytes = new byte[0];
        //创建数据包
        public byte[] Packer(PackHead head, byte[] bodyData)
        {
            byte[] headData = Binary.SerializeByMarshal(head);
            byte[] packData = new byte[headData.Length + bodyData.Length];
            Array.Copy(headData, packData, headData.Length);
            Array.Copy(bodyData, 0, packData, headData.Length, bodyData.Length);
            return packData;
        }
        //解析数据包
        public void UnPack(byte[] data)
        {
            PackHead head = new PackHead();
            int headLength = Binary.SerializeByMarshal(head).Length;

            byte[] totalData = new byte[OtherBytes.Length + data.Length];
            Array.Copy(OtherBytes, totalData, OtherBytes.Length);
            Array.Copy(data, 0, totalData, OtherBytes.Length, data.Length);
            if (totalData.Length < headLength)
            {
                //消息头不足
                OtherBytes = totalData;
                return;
            }
            byte[] headData = new byte[headLength];
            Array.Copy(totalData, headData, headLength);
            head = Binary.DeserializeByMarshal<PackHead>(headData);
            if (totalData.Length < head.BodyLength + headLength)
            {
                //消息体不足
                OtherBytes = totalData;
                return;
            }
            byte[] bodyData = new byte[head.BodyLength];
            Array.Copy(totalData, headLength, bodyData, 0, bodyData.Length);
            //整包发送
            SendPack(head, bodyData);
            int leftLength = totalData.Length - headLength - bodyData.Length;
            OtherBytes = new byte[leftLength];
            if (leftLength > 0)
            {
                //发生粘包
                Array.Copy(totalData, headLength + bodyData.Length, OtherBytes, 0, leftLength);
                if (leftLength > headLength)
                {
                    //拆包
                    UnPack(new byte[0]);
                }
            }
        }
        //发送消息体
        public void SendPack(PackHead head, byte[] bodyData)
        {
            MsgManager.Instance.SendMsg(head.MsgID + "", bodyData);
        }
    }
}
