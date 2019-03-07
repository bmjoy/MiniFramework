using System.Runtime.InteropServices;

namespace MiniFramework
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi,Pack = 4)]
    public class PackHead
    {
        public int MsgID;
        public int UserId;
        public int BodyLength;
        public int TimeStamp;
    }
}

