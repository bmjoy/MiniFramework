using UnityEngine;

namespace MiniFramework
{
    /// <summary>
    /// 帧率计算器
    /// </summary>
    public class FPSCounter
    {
        private const float calcRate = 0.5f;
        private int frameCount = 0;
        private float rateDuration = 0f;
        private int fps = 0;
        public FPSCounter(Console console)
        {
            console.OnUpdateCallback += Update;
            console.OnGUICallback += OnGUI;
        }
        private void Update()
        {
            frameCount++;
            rateDuration += Time.deltaTime;
            if (rateDuration > calcRate)
            {
                fps = (int)(frameCount / rateDuration);
                frameCount = 0;
                rateDuration = 0f;
            }
        }
        private void OnGUI()
        {
            GUILayout.Label("FPS:"+fps);
        }
    }
}
