namespace MiniFramework
{
    public static class ByteUtil
    {
        /// <summary>
        /// 单位换算
        /// </summary>
        /// <param name="size">字节</param>
        /// <returns></returns>
        public static string AutoUnitConversion(long size)
        {
            if (size < 1024L)
            {
                return size.ToString() + " Bytes";
            }

            if (size < 1024L * 1024L)
            {
                return (size / 1024f).ToString("F2") + " KB";
            }

            if (size < 1024L * 1024L * 1024L)
            {
                return (size / 1024f / 1024f).ToString("F2") + " MB";
            }

            if (size < 1024L * 1024L * 1024L * 1024L)
            {
                return (size / 1024f / 1024f / 1024f).ToString("F2") + " GB";
            }

            return (size / 1024f / 1024f / 1024f / 1024f).ToString("F2") + " TB";
        }
    }
}

