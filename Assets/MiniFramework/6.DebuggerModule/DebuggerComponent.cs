using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MiniFramework
{
    public sealed partial class DebuggerComponent : MonoBehaviour
    {
        //调试器默认标题
        static readonly string DefaultWindowTitle = "<b>MiniFramework Debugger</b>";
        //调试器默认大小
        static readonly Rect DefaultWindowRect = new Rect(10f, 10f, 640f, 480f);
        public float DefaultWindowScale = 1f;

        private Rect windowRect = DefaultWindowRect;
        private Rect smallWindowRect = new Rect(10f, 10f, 60f, 60f);
        private Rect dragRect = new Rect(0, 0, float.MaxValue, 25f);
        private bool showSmallWindow = true;
        private List<IDebuggerWindow> windowsList = new List<IDebuggerWindow>();
        private List<string> windowsName = new List<string>();
        private int curSelectedWindowIndex;
        private ConsoleWindow consoleWindow = new ConsoleWindow();
        private ProfilerWindow profilerWindow = new ProfilerWindow();
        private FPSCounter fpsCounter = new FPSCounter();
        private void Awake()
        {
            RegisterDebuggerWindow("<b>Console</b>", consoleWindow);
            RegisterDebuggerWindow("<b>Profiler</b>", profilerWindow);
            RegisterDebuggerWindow("<b>Close</b>", null);
        }
        private void Update(){
            fpsCounter.Update();
        }
        private void OnGUI()
        {
            GUI.skin = GUI.skin;
            GUI.matrix = Matrix4x4.Scale(new Vector3(DefaultWindowScale, DefaultWindowScale, 1f));
            if (showSmallWindow)
            {
                smallWindowRect = GUILayout.Window(0, smallWindowRect, DrawSmallWindow, "<b>Debugger</b>");
            }
            else
            {

                windowRect = GUILayout.Window(0, windowRect, DrawWindow, DefaultWindowTitle);
            }
        }
        //绘制小窗口
        private void DrawSmallWindow(int windowId)
        {
            GUI.DragWindow(dragRect);
            if (GUILayout.Button("FPS:"+fpsCounter.CurFps.ToString("F2"), GUILayout.Width(100f), GUILayout.Height(40f)))
            {
                showSmallWindow = false;
            }
        }
        //绘制默认窗口
        private void DrawWindow(int windowId)
        {
            GUI.DragWindow(dragRect);
            int toolbarIndex = GUILayout.Toolbar(curSelectedWindowIndex, windowsName.ToArray(), GUILayout.Height(30f), GUILayout.MaxWidth(Screen.width));
            if (toolbarIndex >= windowsList.Count)
            {
                showSmallWindow = true;
            }
            else
            {
                windowsList[curSelectedWindowIndex].OnDraw();
                curSelectedWindowIndex = toolbarIndex;
            }
        }

        private void RegisterDebuggerWindow(string title, IDebuggerWindow window)
        {
            windowsName.Add(title);
            if (window != null)
            {
                window.Initialize();
                windowsList.Add(window);
            }
        }
    }

}
