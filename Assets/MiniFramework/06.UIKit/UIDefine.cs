using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniFramework
{
    public enum UIInitType
    {
        //普通
        Normal,
        //常驻
        Fixed,
        //弹出
        Popup
    }
    public enum UIShowMode
    {
        //普通
        Normal,
        //隐藏其他
        HideOther,
    }
    public enum UIAlphaType
    {
        //普通
        Normal,
        //穿透
        Pentrate,
        //不可穿透
        ImPentrate,
    }
}
