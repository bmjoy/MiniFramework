using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
namespace MiniFramework
{
    public class Binary : SerializeUtil
    {
        public override byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public override T Deserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        public byte[] SerializeByMarshal(object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] data = new byte[size];
            IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(obj, bufferIntPtr, true);
                Marshal.Copy(bufferIntPtr, data, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferIntPtr);
            }
            return data;
        }
        public T DeserializeByMarshal<T>(byte[] data)
        {
            object obj;
            int size = Marshal.SizeOf(typeof(T));
            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(data, 0, allocIntPtr, size);
                obj = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)obj;
        }
    }
}