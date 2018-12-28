using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public class Console : MonoSingleton<Console>
    {
        struct ConsoleMessage
        {
            public readonly string message;
            public readonly string stackTrace;
            public readonly LogType type;
            public ConsoleMessage(string message, string stackTrace, LogType type)
            {
                this.message = message;
                this.stackTrace = stackTrace;
                this.type = type;
            }
        }
        public Action OnUpdateCallback;
        public Action OnGUICallback;

        const int margin = 20;
        Rect windowRect = new Rect(margin + Screen.width * 0.5f, margin, Screen.width * 0.5f - (2 * margin), Screen.height - (2 * margin));
        Vector2 scrollPos;
        string content = "";
        float contentWidth = Screen.width * 0.5f - (2 * margin) - 35;
        float contentHeight;

        List<ConsoleMessage> entries = new List<ConsoleMessage>();

        bool scrollToBottom = true;
        bool collapse = false;
        bool showGUI = true;
        bool showKPP = false;

        GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        GUIContent scrollToBottomLabel = new GUIContent("ScrollToBottom", "Scroll bar always at bottom");
        GUIContent showKPPLabel = new GUIContent("ShowKPP", "Show the left param");

        // Use this for initialization
        public override void Awake()
        {
            base.Awake();
            new FPSCounter(this);
            new MemoryDetector(this);
            Application.logMessageReceivedThreaded += HandleLog;
        }
        void OnDestroy()
        {
            Application.logMessageReceivedThreaded -= HandleLog;
        }
        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR||UNITY_STANDALONE
            if (Input.GetKeyUp(KeyCode.F1))
                showGUI = !showGUI;
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 4)
                showGUI = !showGUI;
#endif
            if (OnUpdateCallback != null)
            {
                OnUpdateCallback();
            }
        }
        private void OnGUI()
        {
            GUI.color = Color.red;
            if (!showGUI)
            {
                return;
            }
            if (OnGUICallback != null && showKPP)
            {
                OnGUICallback();
            }

            windowRect = GUI.Window(123, windowRect, ConsoleWindow, "Console");
        }      
        void ConsoleWindow(int windowId)
        {
            if (scrollToBottom)
            {
                scrollPos = Vector2.up * contentHeight;
            }             
            scrollPos = GUI.BeginScrollView(new Rect(10, 20, windowRect.width - 15, windowRect.height - 50), scrollPos, new Rect(0, 0, contentWidth, contentHeight));
            contentHeight = 0;
            GUIStyle style = new GUIStyle();
            for (int i = 0; i < entries.Count; i++)
            {
                ConsoleMessage entry = entries[i];

                if (collapse && i > 0 && entry.message == entries[i - 1].message)
                {
                    continue;
                }
                switch (entry.type)
                {
                    case LogType.Error:
                    case LogType.Exception:
                        style.normal.textColor = Color.red;
                        break;
                    case LogType.Warning:
                        style.normal.textColor = Color.yellow;
                        break;
                    default:
                        style.normal.textColor = Color.white;
                        break;
                }
                content = entry.message + "\n" + entry.stackTrace;
                style.wordWrap = true;
                GUI.Label(new Rect(0, contentHeight, contentWidth, style.CalcHeight(new GUIContent(content), contentWidth)), content, style);
                contentHeight += style.CalcHeight(new GUIContent(content), contentWidth);
            }
            GUI.EndScrollView();
            GUI.contentColor = Color.white;
            GUILayout.BeginArea(new Rect(5, windowRect.height - 25, windowRect.width - 10, 25));
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearLabel))
            {
                entries.Clear();
            }
            collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));
            scrollToBottom = GUILayout.Toggle(scrollToBottom, scrollToBottomLabel, GUILayout.ExpandWidth(false));
            showKPP = GUILayout.Toggle(showKPP, showKPPLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            GUI.DragWindow(new Rect(0, 0, windowRect.width, windowRect.height));
        }
        void HandleLog(string message, string stackTrace, LogType type)
        {
            entries.Add(new ConsoleMessage(message, stackTrace, type));           
        }
    }
}