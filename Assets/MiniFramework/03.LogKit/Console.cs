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
        List<ConsoleMessage> entries = new List<ConsoleMessage>();

        bool scrollToBottom = true;
        bool collapse = false;
        bool showGUI = false;

        GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        GUIContent scrollToBottomLabel = new GUIContent("ScrollToBottom", "Scroll bar always at bottom");
        public void Init() { }
        // Use this for initialization
        void Awake()
        {
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
            if (!showGUI)
            {
                return;
            }
            if (OnGUICallback != null)
            {
                OnGUICallback();
            }
            windowRect = GUILayout.Window(123, windowRect, ConsoleWindow, "Console");
        }
        void ConsoleWindow(int windowId)
        {

            if (scrollToBottom)
            {
                scrollPos = GUILayout.BeginScrollView(Vector2.up * entries.Count * 100.0f);
            }
            else
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);
            }
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
                        GUI.contentColor = Color.red;
                        break;
                    case LogType.Warning:
                        GUI.contentColor = Color.yellow;
                        break;
                    default:
                        GUI.contentColor = Color.white;
                        break;
                }
                GUILayout.Label(entry.message + entry.stackTrace);
            }
            GUILayout.EndScrollView();
            GUI.contentColor = Color.white;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearLabel))
            {
                entries.Clear();
            }
            collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));
            scrollToBottom = GUILayout.Toggle(scrollToBottom, scrollToBottomLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.DragWindow(new Rect(0, 0, windowRect.width, windowRect.height));
        }
        void HandleLog(string message, string stackTrace, LogType type)
        {
            entries.Add(new ConsoleMessage(message, stackTrace, type));
        }
    }
}