using UnityEngine;
namespace MiniFramework
{
    public class MiniApplication : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            Console.Instance.Init();
            UIManager.Instance.Init();
        }
    }
}