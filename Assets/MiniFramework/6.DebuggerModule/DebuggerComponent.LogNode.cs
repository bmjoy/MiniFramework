using System;
using UnityEngine;
namespace MiniFramework
{
    public partial class DebuggerComponent
    {
        private class LogNode:IPoolable
        {
            public DateTime LogTime;
            public LogType LogType;
            public string LogMsg;
            public string StackTrace;

            public bool IsRecycled { get; set; }

            public LogNode Fill(LogType logType,string logMsg,string stackTrace)
            {
                LogTime = DateTime.Now;
                LogType = logType;
                LogMsg = logMsg;
                StackTrace = stackTrace;
                return this;
            }

            public void Clear(){
                LogTime = default(DateTime);
                LogType = default(LogType);
                LogMsg = default(string);
                StackTrace = default(string);
            }

            public void OnRecycled()
            {
                Clear();
            }
        }
    }
}

