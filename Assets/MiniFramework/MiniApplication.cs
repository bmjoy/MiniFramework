using UnityEngine;
namespace MiniFramework
{
    public class MiniApplication : MonoSingleton<MiniApplication>
    {
        // Use this for initialization
        void Start()
        {
            Console.Instance.Init();
        }
        public void SetIsFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
        public void SetResolution(int width,int height)
        {
            Screen.SetResolution(width,height,Screen.fullScreen);
        }
    }
}